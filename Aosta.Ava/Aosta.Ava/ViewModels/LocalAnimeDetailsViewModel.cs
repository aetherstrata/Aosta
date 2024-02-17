// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Ava.ViewModels.DetailsPill;
using Aosta.Data;
using Aosta.Data.Extensions;
using Aosta.Data.Models;

using Avalonia.ReactiveUI;

using DynamicData;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.ViewModels;

public sealed class LocalAnimeDetailsViewModel : ReactiveObject, IRoutableViewModel, IDisposable
{
    private readonly RealmAccess _realm = Locator.Current.GetSafely<RealmAccess>();
    private readonly IDisposable _subscriptionToken;
    private readonly IDisposable _episodesConnection;

    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    public Anime Anime { get; }

    internal LocalAnimeDetailsViewModel(IScreen host, Anime anime)
    {
        Anime = anime;

        HostScreen = host;
        UrlPathSegment = $"anime-details-{anime.ID}";

        Status = Anime.WatchingStatus.Localize();
        DetailsPill = InfoPill.Create(anime);

        _episodesConnection = Anime.Episodes
            .Connect(out _subscriptionToken)
            .Sort(Episode.NumberComparer)
            .ObserveOn(AvaloniaScheduler.Instance)
            .Bind(out _episodes)
            .Subscribe();

        GoToEpisode = ReactiveCommand.CreateFromObservable((Episode episode) =>
            HostScreen.Router.Navigate.Execute(new LocalEpisodeDetailsViewModel(HostScreen, episode, Anime)));
    }

    public IContentInfoPill DetailsPill { get; }

    public string DefaultTitle
    {
        get => Anime.Titles.GetDefault().Title;
        set
        {
            _realm.WriteAsync(r => Anime.Titles.GetDefault().Title = value);
            this.RaisePropertyChanged();
        }
    }

    private readonly ReadOnlyObservableCollection<Episode> _episodes;
    public ReadOnlyObservableCollection<Episode> Episodes => _episodes;

    public string Score => Anime.UserScore?.ToString() ?? LocalizedString.NA;

    public LocalizedString Status { get; }

    public ReactiveCommand<Episode, IRoutableViewModel> GoToEpisode { get; }

    public void RemoveFromRealm()
    {
        _realm.Write(r => r.Remove(Anime));
        HostScreen.Router.NavigateBack.Execute();
    }

    public void Dispose()
    {
        DetailsPill.Dispose();
        Status.Dispose();
        _episodesConnection.Dispose();
        _subscriptionToken.Dispose();
    }
}
