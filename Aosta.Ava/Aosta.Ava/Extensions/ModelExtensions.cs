// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Localization;
using Aosta.Data.Enums;

namespace Aosta.Ava.Extensions;

public static class ModelExtensions
{
    public static LocalizedString Localize(this ContentType type) => type switch
    {
        ContentType.CommercialMessage => new LocalizedString("Enum.AnimeType.CM"),
        ContentType.Movie => new LocalizedString("Enum.AnimeType.Movie"),
        ContentType.Music => new LocalizedString("Enum.AnimeType.Music"),
        ContentType.ONA => new LocalizedString("Enum.AnimeType.ONA"),
        ContentType.OVA => new LocalizedString("Enum.AnimeType.OVA"),
        ContentType.PromotionalVideo => new LocalizedString("Enum.AnimeType.PV"),
        ContentType.Special => new LocalizedString("Enum.AnimeType.Special"),
        ContentType.TV => new LocalizedString("Enum.AnimeType.TV"),
        ContentType.TVSpecial => new LocalizedString("Enum.AnimeType.TVSpecial"),
        _ => LocalizedString.Error(type)
    };

    public static LocalizedString Localize(this ContentType? e) => e switch
    {
        not null => Localize(e.Value),
        null => LocalizedString.NA
    };

    public static LocalizedString Localize(this Season e) => e switch
    {
        Season.Winter => new LocalizedString("Enum.Season.Winter"),
        Season.Spring => new LocalizedString("Enum.Season.Spring"),
        Season.Summer => new LocalizedString("Enum.Season.Summer"),
        Season.Fall => new LocalizedString("Enum.Season.Fall"),
        _ => LocalizedString.Error(e)
    };

    public static LocalizedString Localize(this Season? e) => e switch
    {
        not null => Localize(e.Value),
        null => LocalizedString.NA
    };
}
