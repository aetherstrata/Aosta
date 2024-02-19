// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data;
using Aosta.Data.Extensions;
using Aosta.Data.Models;

using Avalonia.ReactiveUI;

using DynamicData;
using DynamicData.Aggregation;
using DynamicData.Binding;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Realms;

using Splat;

namespace Aosta.Ava.ViewModels;

public sealed class AnimeListPageViewModel : ReactiveObject, IRoutableViewModel, IDisposable
{
    /// <inheritdoc />
    public string UrlPathSegment => "list";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly Realm _realm = Locator.Current.GetSafely<RealmAccess>().GetRealm();
    private readonly IDisposable _notificationToken;

    public AnimeListPageViewModel(IScreen host)
    {
        HostScreen = host;

        var connection = _realm.All<Anime>().Connect(out _notificationToken);

        _countObserver = connection
            .Count()
            .Select(c => c == 0
                ? Localizer.Instance["AnimeList.Header.NoAnime"]
                : string.Format(Localizer.Instance["AnimeList.Header.AnimeCount"], c))
            .ToProperty(this, nameof(AnimeCount), scheduler: AvaloniaScheduler.Instance);

        var observableSearchFilter = this
            .WhenValueChanged(vm => vm.SearchText)
            .Select(x => new Func<Anime, bool>(anime =>
            {
                return string.IsNullOrWhiteSpace(x) || anime.Titles.GetDefault().Title.Contains(x, StringComparison.OrdinalIgnoreCase);
            }));

        _listObserver = connection
            .Sort(Anime.TITLE_COMPARATOR)
            .Filter(observableSearchFilter)
            .ObserveOn(AvaloniaScheduler.Instance)
            .Bind(out _animeList)
            .Subscribe();

        GoToAnime = ReactiveCommand.CreateFromObservable((Anime anime) =>
            HostScreen.Router.Navigate.Execute(new LocalAnimeDetailsViewModel(HostScreen, anime)));
    }

    [Reactive]
    public string? SearchText { get; set; }

    private readonly ObservableAsPropertyHelper<string> _countObserver;
    public string AnimeCount => _countObserver.Value ?? Localizer.Instance["AnimeList.Header.NoAnime"];

    private readonly IDisposable _listObserver;

    private readonly ReadOnlyObservableCollection<Anime> _animeList;
    public ReadOnlyObservableCollection<Anime> AnimeList => _animeList;

    public ReactiveCommand<Anime,IRoutableViewModel> GoToAnime { get; }

    /// <inheritdoc />
    public void Dispose()
    {
        _notificationToken.Dispose();
        _countObserver.Dispose();
        _listObserver.Dispose();
        _realm.Dispose();
    }
}
