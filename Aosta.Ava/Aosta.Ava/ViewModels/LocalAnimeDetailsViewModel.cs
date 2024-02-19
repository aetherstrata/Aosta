// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Aosta.Ava.Controls;
using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Ava.ViewModels.DetailsPill;
using Aosta.Data;
using Aosta.Data.Enums;
using Aosta.Data.Extensions;
using Aosta.Data.Models;

using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.ReactiveUI;

using DynamicData;
using DynamicData.Kernel;

using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Controls.Primitives;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

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

        Anime.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == "watchStatus")
            {
                Status?.Dispose();
                Status = Anime.WatchingStatus.Localize();
            }
        };
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

    [Reactive]
    public LocalizedString? Status { get; set; }

    public ReactiveCommand<Episode, IRoutableViewModel> GoToEpisode { get; }

    public LocalizedData<WatchingStatus>[] StatusList { get; } =
        Enum.GetValues<WatchingStatus>().Select(x => x.LocalizeWithData()).AsArray();

    public LocalizedData<WatchingStatus> ComboBoxStatus
    {
        get => Anime.WatchingStatus.LocalizeWithData();
        set
        {
            _realm.Write(r => Anime.WatchingStatus = value.Data);
            this.RaisePropertyChanged();
        }
    }

    public async Task OpenOnMal()
    {
        if (!string.IsNullOrEmpty(Anime.Url))
        {
            bool res = await Locator.Current.GetSafely<ILauncher>().LaunchUriAsync(new Uri(Anime.Url));
            this.Log().Debug("Open url in browser: {Result}", res);
        }
    }

    public void UpdateScore(double score)
    {
        _realm.Write(r =>
        {
            Anime.UserScore = (int)score;
        });
        this.Log().Debug("Updated user score for {AnimeName}: {Score}", Anime.Titles.GetDefault().Title, Anime.UserScore);
    }

    public void RemoveFromRealm()
    {
        _realm.Write(r => r.Remove(Anime));
        HostScreen.Router.NavigateBack.Execute();
    }

    public void Dispose()
    {
        DetailsPill.Dispose();
        Status?.Dispose();
        _episodesConnection.Dispose();
        _subscriptionToken.Dispose();
    }
}
