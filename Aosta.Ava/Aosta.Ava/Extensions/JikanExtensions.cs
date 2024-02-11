// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Aosta.Ava.Localization;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Ava.Extensions;

public static class JikanExtensions
{
    public static LocalizedString Localize(this AnimeType e) => e switch
    {
        AnimeType.TV => new LocalizedString("Enum.AnimeType.TV"),
        AnimeType.TVSpecial => new LocalizedString("Enum.AnimeType.TVSpecial"),
        AnimeType.OVA => new LocalizedString("Enum.AnimeType.OVA"),
        AnimeType.Movie => new LocalizedString("Enum.AnimeType.Movie"),
        AnimeType.Special => new LocalizedString("Enum.AnimeType.Special"),
        AnimeType.ONA => new LocalizedString("Enum.AnimeType.ONA"),
        AnimeType.Music => new LocalizedString("Enum.AnimeType.Music"),
        AnimeType.PromotionalVideo => new LocalizedString("Enum.AnimeType.PromoVideo"),
        AnimeType.CommercialMessage => new LocalizedString("Enum.AnimeType.Commercial"),
        _ => throw new ArgumentOutOfRangeException(nameof(e), e, $"The passed {nameof(AnimeType)} was invalid")
    };

    public static LocalizedString Localize(this AnimeType? e) => e switch
    {
        not null => Localize(e.Value),
        null => LocalizedString.NA
    };

    public static LocalizedString Localize(this Season e) => e switch
    {
        Season.Spring => new LocalizedString("Enum.Season.Spring"),
        Season.Summer => new LocalizedString("Enum.Season.Summer"),
        Season.Fall => new LocalizedString("Enum.Season.Fall"),
        Season.Winter => new LocalizedString("Enum.Season.Winter"),
        _ => throw new ArgumentOutOfRangeException(nameof(e), e, $"The passed {nameof(Season)} was invalid")
    };

    public static LocalizedString Localize(this Season? e) => e switch
    {
        not null => Localize(e.Value),
        null => LocalizedString.NA
    };

    public static LocalizedData<AnimeTypeFilter> LocalizeWithData(this AnimeTypeFilter filter) => filter switch
    {
        AnimeTypeFilter.All => new LocalizedData<AnimeTypeFilter>(filter, "Label.All"),
        AnimeTypeFilter.TV => new LocalizedData<AnimeTypeFilter>(filter, "Enum.AnimeType.TV"),
        AnimeTypeFilter.TVSpecial => new LocalizedData<AnimeTypeFilter>(filter, "Enum.AnimeType.TVSpecial"),
        AnimeTypeFilter.OVA => new LocalizedData<AnimeTypeFilter>(filter, "Enum.AnimeType.OVA"),
        AnimeTypeFilter.Movie => new LocalizedData<AnimeTypeFilter>(filter, "Enum.AnimeType.Movie"),
        AnimeTypeFilter.Special => new LocalizedData<AnimeTypeFilter>(filter, "Enum.AnimeType.Special"),
        AnimeTypeFilter.ONA => new LocalizedData<AnimeTypeFilter>(filter, "Enum.AnimeType.ONA"),
        AnimeTypeFilter.Music => new LocalizedData<AnimeTypeFilter>(filter, "Enum.AnimeType.Music"),
        AnimeTypeFilter.PromotionalVideo => new LocalizedData<AnimeTypeFilter>(filter, "Enum.AnimeType.PromoVideo"),
        AnimeTypeFilter.CommercialMessage => new LocalizedData<AnimeTypeFilter>(filter, "Enum.AnimeType.Commercial"),
        _ => throw new ArgumentOutOfRangeException(nameof(filter), filter,
            $"The passed {nameof(AnimeTypeFilter)} was invalid")
    };

    public static LocalizedData<AiringStatusFilter> LocalizeWithData(this AiringStatusFilter filter) => filter switch
    {
        AiringStatusFilter.All => new LocalizedData<AiringStatusFilter>(filter, "Label.All"),
        AiringStatusFilter.Airing => new LocalizedData<AiringStatusFilter>(filter, "Enum.AiringStatus.Airing"),
        AiringStatusFilter.Complete => new LocalizedData<AiringStatusFilter>(filter, "Enum.AiringStatus.Complete"),
        AiringStatusFilter.Upcoming => new LocalizedData<AiringStatusFilter>(filter, "Enum.AiringStatus.Upcoming"),
        _ => throw new ArgumentOutOfRangeException(nameof(filter), filter,
            $"The passed {nameof(AiringStatusFilter)} was invalid")
    };

    public static LocalizedData<AnimeAgeRatingFilter> LocalizeWithData(this AnimeAgeRatingFilter filter) => filter switch
    {
        AnimeAgeRatingFilter.All => new LocalizedData<AnimeAgeRatingFilter>(filter, "Label.All"),
        AnimeAgeRatingFilter.G => new LocalizedData<AnimeAgeRatingFilter>(filter, "Enum.AnimeAgeRatingFilter.G"),
        AnimeAgeRatingFilter.PG => new LocalizedData<AnimeAgeRatingFilter>(filter, "Enum.AnimeAgeRatingFilter.PG"),
        AnimeAgeRatingFilter.PG13 => new LocalizedData<AnimeAgeRatingFilter>(filter, "Enum.AnimeAgeRatingFilter.PG13"),
        AnimeAgeRatingFilter.R17 => new LocalizedData<AnimeAgeRatingFilter>(filter, "Enum.AnimeAgeRatingFilter.R17"),
        AnimeAgeRatingFilter.R => new LocalizedData<AnimeAgeRatingFilter>(filter, "Enum.AnimeAgeRatingFilter.R"),
        AnimeAgeRatingFilter.RX => new LocalizedData<AnimeAgeRatingFilter>(filter, "Enum.AnimeAgeRatingFilter.RX"),
        _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, null)
    };

    public static LocalizedString LocalizeEpisodeNumber(this AnimeEpisodeResponse response)
    {
        return new LocalizedString("Label.Episode.Number", response.MalId);
    }
}
