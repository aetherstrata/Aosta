// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Localization;
using Aosta.Jikan.Enums;

namespace Aosta.Ava.Extensions;

public static class JikanExtensions
{
    public static LocalizedString Localize(this AnimeType e) => e switch
    {
        AnimeType.TV => new LocalizedString("Jikan.AnimeType.TvAnime"),
        AnimeType.TVSpecial => new LocalizedString("Jikan.AnimeType.TvSpecial"),
        AnimeType.OVA => new LocalizedString("Jikan.AnimeType.OVA"),
        AnimeType.Movie => new LocalizedString("Jikan.AnimeType.Movie"),
        AnimeType.Special => new LocalizedString("Jikan.AnimeType.Special"),
        AnimeType.ONA => new LocalizedString("Jikan.AnimeType.ONA"),
        AnimeType.Music => new LocalizedString("Jikan.AnimeType.Music"),
        AnimeType.PromotionalVideo => new LocalizedString("Jikan.AnimeType.PromoVideo"),
        AnimeType.CommercialMessage => new LocalizedString("Jikan.AnimeType.Commercial"),
        _ => LocalizedString.Error(e)
    };

    public static LocalizedString Localize(this Season e) => e switch
    {
        Season.Spring => new LocalizedString("Jikan.Season.Spring"),
        Season.Summer => new LocalizedString("Jikan.Season.Summer"),
        Season.Fall => new LocalizedString("Jikan.Season.Fall"),
        Season.Winter => new LocalizedString("Jikan.Season.Winter"),
        _ => LocalizedString.Error(e)
    };
}
