using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Core.Database;
using Aosta.Core.Database.Models;
using Aosta.Jikan;

using Avalonia.ReactiveUI;

using DynamicData;

using ReactiveUI;

using Realms;

using Splat;

namespace Aosta.Ava.ViewModels;

public class SearchPageViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string UrlPathSegment => "search";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly IJikan _client = Locator.Current.GetSafely<IJikan>();
    private readonly Serilog.ILogger _logger = Locator.Current.GetSafely<Serilog.ILogger>();
    private readonly RealmAccess _realm = Locator.Current.GetSafely<RealmAccess>();

    public SearchPageViewModel(IScreen screen)
    {
        HostScreen = screen;

        this.WhenAnyValue(vm => vm.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(AvaloniaScheduler.Instance)
            .Subscribe(s => Task.Run(() => executeSearch(s)));
    }

    private async Task executeSearch(string s)
    {
        IsBusy = true;
        SearchResults.Clear();

        if (!string.IsNullOrWhiteSpace(s))
        {
            try
            {
                var result = await _client.SearchAnimeAsync(s);

                // Mongo please, for the love of God, add better support for LINQ 😭
                var added = _realm.Run(r => r.All<Anime>()
                    .Filter($"{nameof(Anime.Jikan)}.{nameof(Anime.Jikan.ID)} IN {{{string.Join(',', result.Data.Select(x => x.MalId))}}}")
                    .AsRealmCollection()
                    .Select(x => x.Jikan!.ID)
                    .ToHashSet());

                SearchResults.AddRange(result.Data.Select(response =>
                    new AnimeSearchResultViewModel(response, added.Contains(response.MalId))));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Anime search query failed for string: {SearchText}", SearchText);
            }
        }

        IsBusy = false;
    }

    private bool _isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }

    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    public ObservableCollection<AnimeSearchResultViewModel> SearchResults { get; } = [];
}
