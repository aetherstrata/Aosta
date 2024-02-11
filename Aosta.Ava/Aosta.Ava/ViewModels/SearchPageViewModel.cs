using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data;
using Aosta.Data.Extensions;
using Aosta.Jikan;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

using Avalonia.ReactiveUI;

using DynamicData;
using DynamicData.Kernel;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Realms;

using Splat;

using Anime = Aosta.Data.Models.Anime;
using Setting = Aosta.Ava.Settings.Setting;

namespace Aosta.Ava.ViewModels;

public class SearchPageViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string UrlPathSegment => "search";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly IJikan _client = Locator.Current.GetSafely<IJikan>();
    private readonly RealmAccess _realm = Locator.Current.GetSafely<RealmAccess>();

    [Reactive]
    public bool IsBusy { get; set; }

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    public ObservableCollection<AnimeSearchResultViewModel> SearchResults { get; } = [];

    public LocalizedData<AnimeTypeFilter>[] AnimeTypes { get; } =
        Enum.GetValues<AnimeTypeFilter>().Select(x => x.LocalizeWithData()).AsArray();

    [Reactive]
    public LocalizedData<AnimeTypeFilter> AnimeTypeFilter { get; set; } = Jikan.Query.Enums.AnimeTypeFilter.All.LocalizeWithData();

    [Reactive]
    public double MinScore { get; set; } = 0;

    [Reactive]
    public double MaxScore { get; set; } = 10;

    public LocalizedData<AiringStatusFilter>[] StatusList { get; } =
        Enum.GetValues<AiringStatusFilter>().Select(x => x.LocalizeWithData()).AsArray();

    [Reactive]
    public LocalizedData<AiringStatusFilter> StatusFilter { get; set; } = AiringStatusFilter.All.LocalizeWithData();

    public LocalizedData<AnimeAgeRatingFilter>[] AgeRatingList { get; } =
        Enum.GetValues<AnimeAgeRatingFilter>().Select(x => x.LocalizeWithData()).AsArray();

    [Reactive]
    public LocalizedData<AnimeAgeRatingFilter> RatingFilter { get; set; } = AnimeAgeRatingFilter.All.LocalizeWithData();

    public SearchPageViewModel(IScreen screen)
    {
        HostScreen = screen;

        var searchCommand = ReactiveCommand.CreateFromTask((string s, CancellationToken ct) => executeSearch(s, ct));

        this.WhenAnyValue(static vm => vm.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(AvaloniaScheduler.Instance)
            .InvokeCommand(searchCommand);
    }

    private async Task executeSearch(string s, CancellationToken ct = default)
    {
        IsBusy = true;

        if (!string.IsNullOrWhiteSpace(s))
        {
            try
            {
                var queryParams = AnimeSearchQueryParameters.Create()
                    .Query(s)
                    .Type(AnimeTypeFilter.Data)
                    .Status(StatusFilter.Data)
                    .Rating(RatingFilter.Data)
                    .MinScore(MinScore)
                    .MaxScore(MaxScore)
                    .SafeForWork(!Setting.IncludeNsfw)
                    .Unapproved(Setting.IncludeUnapproved);

                var result = await _client.SearchAnimeAsync(queryParams, ct);
                var resultIds = result.Data.Select(static x => x.MalId);

                // Check if the MAL IDs returned from Jikan appear in Realm
                var found = _realm.Run(r =>
                    r.All<Anime>()
                        .In(x => x.Jikan!.ID, resultIds)
                        .AsRealmCollection()
                        .Select(x => x.Jikan!.ID)
                        .ToHashSet());

                var viewModels = result.Data
                    .Select(response => new AnimeSearchResultViewModel(HostScreen, response, found.Contains(response.MalId)));

                SearchResults.Clear();
                SearchResults.AddRange(viewModels);
            }
            catch (Exception ex)
            {
                this.Log().Error(ex, "Anime search query failed for string: {SearchText}", SearchText);
            }
        }

        IsBusy = false;
    }
}
