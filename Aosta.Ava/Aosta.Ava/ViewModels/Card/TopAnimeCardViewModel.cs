// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reactive;

using Aosta.Jikan.Models.Response;

using ReactiveUI;

namespace Aosta.Ava.ViewModels.Card;

public class TopAnimeCardViewModel(IScreen host, AnimeResponse response) : ReactiveObject, IOnlineCardViewModel
{
    public string BannerUrl { get; } = response.Images?.JPG?.ImageUrl ?? IOnlineCardViewModel.PORTRAIT_PLACEHOLDER;

    public ReactiveCommand<Unit, IRoutableViewModel> GoToDetails { get; } =
        ReactiveCommand.CreateFromObservable(() =>
            host.Router.Navigate.Execute(new JikanAnimeDetailsViewModel(host, response)));

    public string Score { get; } = response.Score?.ToString("0.00") ?? "N/A";
}
