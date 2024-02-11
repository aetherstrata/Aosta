// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Ava.ViewModels.Card;
using Aosta.Data;

using Avalonia.ReactiveUI;

using DynamicData;
using DynamicData.Aggregation;

using ReactiveUI;

using Realms;

using Splat;

using Anime = Aosta.Data.Models.Anime;

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

        _listObserver = connection
            .Sort(Anime.TITLE_COMPARATOR)
            .Transform(model => new AnimeListCardViewModel(HostScreen, model))
            .ObserveOn(AvaloniaScheduler.Instance)
            .Bind(out _animeList)
            .Subscribe();
    }

    private readonly ObservableAsPropertyHelper<string> _countObserver;
    public string AnimeCount => _countObserver.Value ?? Localizer.Instance["AnimeList.Header.NoAnime"];

    private readonly IDisposable _listObserver;

    private readonly ReadOnlyObservableCollection<AnimeListCardViewModel> _animeList;
    public ReadOnlyObservableCollection<AnimeListCardViewModel> AnimeList => _animeList;

    /// <inheritdoc />
    public void Dispose()
    {
        _notificationToken.Dispose();
        _countObserver.Dispose();
        _listObserver.Dispose();
        _realm.Dispose();
    }
}
