using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Aosta.Core;
using Aosta.Jikan;
using Aosta.Jikan.Models.Response;

using Avalonia.ReactiveUI;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class SearchPageViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment => "search";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly AostaDotNet _aosta;
    private readonly IJikan _client;

    public SearchPageViewModel(IScreen screen, AostaDotNet aosta)
    {
        HostScreen = screen;

        _aosta = aosta;
        _client = new JikanConfiguration().Build();

        this.WhenAnyValue(vm => vm.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(AvaloniaScheduler.Instance)
            .Subscribe(s => _ = executeSearch(s));
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
                var vm = new AnimeSearchResultViewModel(anime, _aosta);
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
