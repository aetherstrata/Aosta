// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Threading.Tasks;

using Aosta.Ava.Localization;
using Aosta.Core;
using Aosta.Core.Database.Mapper;
using Aosta.Core.Database.Models.Jikan;
using Aosta.Core.Extensions;
using Aosta.Jikan.Models.Response;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class JikanAnimeDetailsViewModel(IScreen hostScreen, AnimeResponse response, AostaDotNet aosta) : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment { get; } = $"jikan-details-{response.MalId}";

    /// <inheritdoc />
    public IScreen HostScreen { get; } = hostScreen;

    private readonly JikanAnime _response = response.ToJikanAnime();

    private readonly AostaDotNet _aosta = aosta;

    public string Title => _response.Titles.GetDefault();

    public string Synopsis => _response.Synopsis ?? Localizer.Instance["Label.NotAvailable"];

    public string? Banner => _response.Images?.GetDefault();

}
