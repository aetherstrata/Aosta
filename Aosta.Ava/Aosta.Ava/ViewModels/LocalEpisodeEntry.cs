// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reactive;
using System.Reactive.Linq;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data;
using Aosta.Data.Extensions;
using Aosta.Data.Models;
using Aosta.Jikan;

using Avalonia.Controls.Primitives;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.ViewModels;

public class LocalEpisodeEntry
{
    internal Episode Episode { get; }

    public LocalEpisodeEntry(IScreen host, Episode episode, Anime anime)
    {
        Episode = episode;

        GoToDetails = ReactiveCommand.CreateFromObservable(() =>
            host.Router.Navigate.Execute(new LocalEpisodeDetailsViewModel(host, Episode, anime)));
    }

    internal ReactiveCommand<Unit, IRoutableViewModel> GoToDetails { get; }

    public string Title => Episode.Titles.GetDefault().Title ?? LocalizedString.NOT_AVAILABLE;
}
