// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Reactive;

using Aosta.Ava.Localization;
using Aosta.Jikan.Models.Response;

using ReactiveUI;

namespace Aosta.Ava.ViewModels.Card;

public class JikanAnimeCardViewModel : ReactiveObject, IOnlineCard
{
    public JikanAnimeCardViewModel(IScreen host, AnimeResponse response)
    {
        BannerUrl = response.Images?.JPG?.ImageUrl ?? IOnlineCard.PORTRAIT_PLACEHOLDER;
        GoToDetails = ReactiveCommand.CreateFromObservable(() => host.Router.Navigate.Execute(new OnlineAnimeDetailsViewModel(host, response)));
        Score = response.Score?.ToString("0.00") ?? LocalizedString.NA;
    }

    public string BannerUrl { get; }

    public ReactiveCommand<Unit, IRoutableViewModel> GoToDetails { get; }

    public string Score { get; }
}
