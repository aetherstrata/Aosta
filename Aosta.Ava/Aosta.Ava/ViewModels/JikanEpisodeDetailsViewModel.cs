// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Jikan.Models.Response;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class JikanEpisodeDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly AnimeEpisodeResponse _response;

    public JikanEpisodeDetailsViewModel(IScreen host, AnimeEpisodeResponse response, long animeId)
    {
        _response = response;

        HostScreen = host;
        UrlPathSegment = $"jikan-ep-{animeId}-{_response.MalId}";
    }

    internal bool HasSynopsis => !string.IsNullOrEmpty(_response.Synopsis);

    internal string PageTitle => _response.LocalizeEpisodeNumber();

    internal string Title => _response.Title ?? $"{_response.MalId} - {LocalizedString.NOT_AVAILABLE}";

    internal string? Synopsis => _response.Synopsis;
}
