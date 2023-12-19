using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Core;
using Aosta.Jikan;

using Avalonia.ReactiveUI;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.ViewModels;

public class SearchPageViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string UrlPathSegment => "search";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly IJikan _client = Locator.Current.GetSafely<IJikan>();

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
            var result = await _client.SearchAnimeAsync(s);

            foreach (var anime in result.Data)
            {
                var vm = new AnimeSearchResultViewModel(anime);
                SearchResults.Add(vm);
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
