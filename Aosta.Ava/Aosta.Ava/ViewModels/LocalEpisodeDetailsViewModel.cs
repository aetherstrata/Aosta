// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data;
using Aosta.Data.Enums;
using Aosta.Data.Extensions;
using Aosta.Data.Models;
using Aosta.Jikan;

using Avalonia.ReactiveUI;

using DynamicData;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Realms;

using Splat;

namespace Aosta.Ava.ViewModels;

public sealed class LocalEpisodeDetailsViewModel : ReactiveObject, IRoutableViewModel, IDisposable
{
    private readonly RealmAccess _realm = Locator.Current.GetSafely<RealmAccess>();
    private readonly IDisposable _subscriptionToken;
    private readonly IDisposable _notesConnection;

    private readonly Anime _anime;

    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    internal Episode Episode { get; }

    internal LocalEpisodeDetailsViewModel(IScreen host, Episode episode, Anime anime)
    {
        HostScreen = host;
        UrlPathSegment = $"local-ep-{anime.PrimaryKey}-{episode.Number}";

        Episode = episode;
        _anime = anime;

        if (_anime.ID.HasValue && Episode.Synopsis is null)
        {
            Observable.StartAsync(async () =>
            {
                var jikan = Locator.Current.GetSafely<IJikan>();
                var response = await jikan.GetAnimeEpisodeAsync(_anime.ID.Value, Episode.Number);

                _realm.Write(r =>
                {
                    Episode.Synopsis = response.Data.Synopsis;
                    Episode.OnlineScore = response.Data.Score;
                    Episode.Duration = response.Data.Duration;
                });
                localizeDuration();
            });
        }

        _notesConnection = Episode.Notes
            .Connect(out _subscriptionToken)
            .Sort(EpisodeNote.PointInTimeComparer)
            .ObserveOn(AvaloniaScheduler.Instance)
            .Bind(out _episodeNotes)
            .Subscribe();

        PageTitle = ("Label.Episode.Number", Episode.Number).Localize();
        localizeDuration();
    }

    private void localizeDuration()
    {
        Duration?.Dispose();
        Duration = Episode.Duration.HasValue
            ? LocalizedString.Duration(TimeSpan.FromSeconds(Episode.Duration.Value))
            : LocalizedString.NOT_AVAILABLE;
        this.RaisePropertyChanged(nameof(Duration));
    }

    internal LocalizedString PageTitle { get; }

    internal string Title => Episode.Titles.GetDefault().Title ?? $"{Episode.Number} - {LocalizedString.NOT_AVAILABLE}";

    internal LocalizedString? Duration { get; set; }

    [Reactive]
    internal double NewNoteTimeValue { get; set; }

    [Reactive]
    internal string? NewNoteTitle { get; set; }

    [Reactive]
    internal string? NewNoteText { get; set; }

    private readonly ReadOnlyObservableCollection<EpisodeNote> _episodeNotes;
    internal ReadOnlyObservableCollection<EpisodeNote> EpisodeNotes => _episodeNotes;

    internal void AddNote()
    {
        _realm.Write(r => Episode.Notes.Add(new EpisodeNote
        {
            PointInTime = TimeSpan.FromSeconds(NewNoteTimeValue),
            Title = NewNoteTitle,
            Note = NewNoteText
        }));
    }

    internal Task OpenMalUrl()
    {
        return Locator.Current.GetSafely<ILauncher>().LaunchUriAsync(new Uri(Episode.Url!));
    }

    internal void MarkAsWatched()
    {
        _realm.Write(r =>
        {
            Episode.Watched = DateTimeOffset.Now;

            if (_anime.Episodes.Any(x => !x.Watched.HasValue))
            {
                _anime.WatchingStatus = WatchingStatus.Watching;
            }
            else
            {
                _anime.WatchingStatus = WatchingStatus.Completed;
            }
        });
    }

    internal void DeleteNote(EpisodeNote note)
    {
        _realm.Write(r => Episode.Notes.Remove(note));
    }

    public void Dispose()
    {
        PageTitle.Dispose();
        Duration.Dispose();
        _notesConnection.Dispose();
        _subscriptionToken.Dispose();
    }
}
