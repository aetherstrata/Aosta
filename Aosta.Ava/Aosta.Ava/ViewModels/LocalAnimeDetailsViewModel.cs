// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Localization;
using Aosta.Ava.ViewModels.DetailsPill;
using Aosta.Data.Extensions;
using Aosta.Data.Models;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class LocalAnimeDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly Anime _anime;

    internal LocalAnimeDetailsViewModel(IScreen host, Anime anime)
    {
        _anime = anime;

        HostScreen = host;
        UrlPathSegment = $"anime-details-{anime.ID}";

        DetailsPill = InfoPill.Create(anime);
    }

    public IContentInfoPill DetailsPill { get; }

    internal string? LargeBanner => _anime.Jikan?.Images?.JPG?.LargeImageUrl;

    internal string Synopsis => _anime.Synopsis ?? LocalizedString.NOT_AVAILABLE;

    internal string Title => _anime.Local?.Title ?? _anime.Jikan?.Titles.GetDefault() ?? LocalizedString.NA;
}
