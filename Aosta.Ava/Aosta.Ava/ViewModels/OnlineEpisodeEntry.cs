// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reactive;

using Aosta.Ava.Localization;
using Aosta.Jikan.Models.Response;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class OnlineEpisodeEntry
{
    internal AnimeEpisodeResponse Response { get; }

    public OnlineEpisodeEntry(IScreen host, AnimeEpisodeResponse response, long animeId)
    {
        Response = response;

        GoToDetails = ReactiveCommand.CreateFromObservable(() =>
            host.Router.Navigate.Execute(new OnlineEpisodeDetailsViewModel(host, Response, animeId)));
    }

    internal ReactiveCommand<Unit, IRoutableViewModel> GoToDetails { get; }

    public string Title => Response.Title ?? LocalizedString.NOT_AVAILABLE;
}
