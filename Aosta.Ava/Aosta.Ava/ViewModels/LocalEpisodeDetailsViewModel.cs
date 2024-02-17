// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data;
using Aosta.Data.Extensions;
using Aosta.Data.Models;
using Aosta.Jikan;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.ViewModels;

public class LocalEpisodeDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly RealmAccess _realm = Locator.Current.GetSafely<RealmAccess>();

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

        if (anime.ID.HasValue && Episode.Synopsis is null)
        {
            Observable.StartAsync(async () =>
            {
                var jikan = Locator.Current.GetSafely<IJikan>();
                var response = await jikan.GetAnimeEpisodeAsync(anime.ID.Value, Episode.Number);

                _realm.Write(r =>
                {
                    Episode.Synopsis = response.Data.Synopsis;
                    Episode.OnlineScore = response.Data.Score;
                    Episode.Duration = response.Data.Duration;
                });
            });
        }

        PageTitle = ("Label.Episode.Number", Episode.Number).Localize();
        Duration = Episode.Duration.HasValue
            ? LocalizedString.Duration(TimeSpan.FromSeconds(Episode.Duration.Value))
            : LocalizedString.NOT_AVAILABLE;
    }

    internal ILocalized PageTitle { get; }

    internal string Title => Episode.Titles.GetDefault().Title ?? $"{Episode.Number} - {LocalizedString.NOT_AVAILABLE}";

    internal ILocalized Duration { get; }

    internal void MarkAsWatched()
    {
        _realm.Write(r =>
        {
            Episode.Watched = DateTimeOffset.Now;
        });
    }
}
