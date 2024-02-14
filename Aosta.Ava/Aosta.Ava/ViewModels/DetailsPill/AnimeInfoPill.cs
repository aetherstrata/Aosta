// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data.Models;
using Aosta.Jikan.Models.Response;

using ReactiveUI.Fody.Helpers;

namespace Aosta.Ava.ViewModels.DetailsPill;

public class AnimePill : EpisodesPill
{
    public LocalizedString Season { get; set; }

    [Reactive]
    public string Year { get; set; }

    public AnimePill(AnimeResponse response) : base(response)
    {
        Season = response.Season.Localize();
        Year = response.Year.ToString() ?? LocalizedString.NA;
    }

    public AnimePill(Anime model) : base(model)
    {
        Season = model.Season.Localize();
        Year = model.Year.ToString() ?? LocalizedString.NA;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Season.Dispose();
        }

        base.Dispose(disposing);
    }
}
