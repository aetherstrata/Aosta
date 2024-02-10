// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Jikan.Models.Response;

using Anime = Aosta.Data.Models.Anime;

namespace Aosta.Ava.ViewModels.Details;

public class AnimeSeasonInfoPill : InfoPill
{
    public LocalizedString Season { get; set; }

    public AnimeSeasonInfoPill(AnimeResponse response) : base(response)
    {
        Season = response.Season.Localize();
    }

    public AnimeSeasonInfoPill(Anime model) : base(model)
    {
        Season = model.Season.Localize();
    }
}
