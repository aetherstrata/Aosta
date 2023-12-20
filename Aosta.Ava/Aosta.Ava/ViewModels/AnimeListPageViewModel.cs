// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Core.Database;
using Aosta.Core.Database.Models;

using Avalonia.ReactiveUI;

using DynamicData;
using DynamicData.Aggregation;
using DynamicData.Binding;

using ReactiveUI;

using Realms;

using Splat;

namespace Aosta.Ava.ViewModels;

public sealed class AnimeListPageViewModel : ReactiveObject, IRoutableViewModel, IDisposable
{
    /// <inheritdoc />
    public string? UrlPathSegment => "list";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly RealmAccess _realm = Locator.Current.GetSafely<RealmAccess>();

    public AnimeListPageViewModel(IScreen host)
    {
        HostScreen = host;

        _countObserver = _realm.Connect<Anime>()
            .Count()
            .Select(c => c == 0
                ? Localizer.Instance["AnimeList.Header.NoAnime"]
                : string.Format(Localizer.Instance["AnimeList.Header.AnimeCount"], c))
            .ToProperty(this, x => x.AnimeCount);

        _listObserver = _realm.Connect<Anime>()
            .Sort(Anime.TITLE_COMPARATOR)
            .Transform(model => new AnimeListCardViewModel(HostScreen, model))
            .ObserveOn(AvaloniaScheduler.Instance)
            .Bind(out _animeList)
            .Subscribe();
    }

    private readonly ObservableAsPropertyHelper<string> _countObserver;
    public string AnimeCount => _countObserver.Value;

    private readonly IDisposable _listObserver;

    private readonly ReadOnlyObservableCollection<AnimeListCardViewModel> _animeList;
    public ReadOnlyObservableCollection<AnimeListCardViewModel> AnimeList => _animeList;

    /// <inheritdoc />
    public void Dispose()
    {
        _countObserver.Dispose();
        _listObserver.Dispose();
    }
}
