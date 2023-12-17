// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using System.Reactive;

using Aosta.Ava.Extensions;
using Aosta.Core;
using Aosta.Jikan.Models.Response;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.ViewModels;

public class AnimeCardViewModel : ReactiveObject
{
    public AnimeCardViewModel(IScreen host, AnimeResponse response)
    {
        GoToDetails = ReactiveCommand.CreateFromObservable(() =>
                host.Router.Navigate.Execute(new JikanAnimeDetailsViewModel(host, response)));

        Banner = response.Images?.JPG?.ImageUrl;
        Title = response.Titles.First(entry => entry.Type == "Default").Title;
        Score = response.Score?.ToString("0.00") ?? "N/A";
    }

    public ReactiveCommand<Unit, IRoutableViewModel> GoToDetails { get; }

    public string? Banner { get; }

    public string? Title { get; }

    public string Score { get; }
}
