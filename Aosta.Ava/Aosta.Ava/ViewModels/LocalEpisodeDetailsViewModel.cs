// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data.Extensions;
using Aosta.Data.Models;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class LocalEpisodeDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    public Episode Episode { get; }

    internal LocalEpisodeDetailsViewModel(IScreen host, Episode episode, Anime anime)
    {
        HostScreen = host;
        UrlPathSegment = $"local-ep-{anime.PrimaryKey}-{episode.Number}";

        Episode = episode;
        PageTitle = ("Label.Episode.Number", Episode.Number).Localize();
        Duration = Episode.Duration.HasValue
            ? LocalizedString.Duration(TimeSpan.FromSeconds(Episode.Duration.Value))
            : LocalizedString.NOT_AVAILABLE;
    }

    internal ILocalized PageTitle { get; }

    internal string Title => Episode.Titles.GetDefault().Title ?? $"{Episode.Number} - {LocalizedString.NOT_AVAILABLE}";

    internal ILocalized Duration { get; }
}
