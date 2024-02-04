using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Data;
using Aosta.Data.Database.Models;
using Aosta.Data.Extensions;
using Aosta.Jikan;
using Aosta.Jikan.Query.Parameters;

using Avalonia.ReactiveUI;

using DynamicData;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Realms;

using Splat;

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

    [Reactive]
    public bool IsFilterPaneOpen { get; set; }

    public ObservableCollection<AnimeSearchResultViewModel> SearchResults { get; } = [];

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
                    .SafeForWork(!_realm.GetSetting(Setting.INCLUDE_NSFW, false));

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
                    .Select(response => new AnimeSearchResultViewModel(response, found.Contains(response.MalId)));

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

    public void ToggleFilterMenu()
    {
        IsFilterPaneOpen = !IsFilterPaneOpen;
    }
}
