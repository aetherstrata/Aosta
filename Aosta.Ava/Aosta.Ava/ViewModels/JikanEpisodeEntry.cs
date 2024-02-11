// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reactive;

using Aosta.Ava.Localization;
using Aosta.Jikan.Models.Response;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class JikanEpisodeEntry
{
    private readonly AnimeEpisodeResponse _response;

    public JikanEpisodeEntry(IScreen host, AnimeEpisodeResponse response, long animeId)
    {
        _response = response;

        GoToDetails = ReactiveCommand.CreateFromObservable(() =>
            host.Router.Navigate.Execute(new JikanEpisodeDetailsViewModel(host, _response, animeId)));
    }

    internal ReactiveCommand<Unit, IRoutableViewModel> GoToDetails { get; }

    public long Number => _response.MalId;
    public string Title => _response.Title ?? LocalizedString.NOT_AVAILABLE;
    public bool? Filler => _response.Filler;
    public bool? Recap => _response.Recap;
}
