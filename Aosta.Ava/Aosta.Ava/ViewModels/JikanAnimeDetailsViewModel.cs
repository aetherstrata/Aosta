// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reactive;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Core;
using Aosta.Core.Database.Mapper;
using Aosta.Core.Database.Models.Jikan;
using Aosta.Core.Extensions;
using Aosta.Jikan.Models.Response;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.ViewModels;

public class JikanAnimeDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly JikanAnime _response;

    public JikanAnimeDetailsViewModel(IScreen hostScreen, AnimeResponse response)
    {
        UrlPathSegment = $"jikan-details-{response.MalId}";
        HostScreen = hostScreen;
        GoBack = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.NavigateBack.Execute(Unit.Default))!;

        _response = response.ToJikanAnime();
    }

    public string Title => _response.Titles.GetDefault();

    public string Synopsis => _response.Synopsis ?? Localizer.Instance["Label.NotAvailable"];

    public string? Banner => _response.Images?.GetDefault();

    public ReactiveCommand<Unit, IRoutableViewModel> GoBack { get; }
}
