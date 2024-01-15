using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Core;
using Aosta.Core.Database.Models;
using Aosta.Core.Extensions;
using Aosta.Jikan;

using Avalonia.ReactiveUI;

using DynamicData;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

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
    private readonly AostaDotNet _aosta = Locator.Current.GetSafely<AostaDotNet>();

    [Reactive] public bool IsBusy { get; set; }

    [Reactive] public string SearchText { get; set; } = string.Empty;

    [Reactive] public bool IsFilterPaneOpen { get; set; }

    public ObservableCollection<AnimeSearchResultViewModel> SearchResults { get; } = [];


    public SearchPageViewModel(IScreen screen)
    {
        HostScreen = screen;

        var searchCommand = ReactiveCommand.CreateFromTask((string s, CancellationToken ct) => executeSearch(s, ct));

        this.WhenAnyValue(vm => vm.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(AvaloniaScheduler.Instance)
            .InvokeCommand(searchCommand);
    }

    private async Task executeSearch(string s, CancellationToken ct = default)
    {
        IsBusy = true;
        SearchResults.Clear();

        if (!string.IsNullOrWhiteSpace(s))
        {
            try
            {
                var result = await _client.SearchAnimeAsync(s, ct);
                var resultIds = result.Data.Select(x => x.MalId);

                var added = _aosta.Realm.Run(r => r.All<Anime>()
                    .In(x => x.Jikan!.ID, resultIds)
                    .AsRealmCollection()
                    .Select(x => x.Jikan!.ID)
                    .ToHashSet());

                SearchResults.AddRange(result.Data.Select(response =>
                    new AnimeSearchResultViewModel(response, added.Contains(response.MalId))));
            }
            catch (Exception ex)
            {
                _aosta.Log.Error(ex, "Anime search query failed for string: {SearchText}", SearchText);
            }
        }

        IsBusy = false;
    }

    public void ToggleFilterMenu()
    {
        IsFilterPaneOpen = !IsFilterPaneOpen;
    }
}
