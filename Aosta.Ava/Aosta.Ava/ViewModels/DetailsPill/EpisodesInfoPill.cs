// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Data.Models;
using Aosta.Jikan.Models.Response;

using ReactiveUI.Fody.Helpers;

namespace Aosta.Ava.ViewModels.DetailsPill;

public class EpisodesPill : InfoPill
{
    [Reactive]
    public int Episodes { get; set; }

    public EpisodesPill(AnimeResponse response) : base(response)
    {
        Episodes = response.Episodes.GetValueOrDefault();
    }

    public EpisodesPill(Anime model) : base(model)
    {
        Episodes = model.Episodes.Count;
    }
}
