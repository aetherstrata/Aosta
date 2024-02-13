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
    public static LocalizedString Localize(this AiringStatus e) => e switch
    {
        AiringStatus.Airing => new LocalizedString("Enum.AiringStatus.Airing"),
        AiringStatus.Completed => new LocalizedString("Enum.AiringStatus.Finished"),
        AiringStatus.Upcoming => new LocalizedString("Enum.AiringStatus.Upcoming"),
        _ => throw new ArgumentOutOfRangeException(nameof(e), e, nameof(AiringStatus))
    };

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
        _ => throw new ArgumentOutOfRangeException(nameof(e), e, error_message(nameof(AnimeType)))
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
        _ => throw new ArgumentOutOfRangeException(nameof(e), e, error_message(nameof(Season)))
    };

    public static LocalizedString Localize(this Season? e) => e switch
    {
        not null => Localize(e.Value),
        null => LocalizedString.NA
    };

    public static LocalizedData<SortDirection> LocalizeWithData(this SortDirection direction) => direction switch
    {
        SortDirection.Ascending => new LocalizedData<SortDirection>(direction, "Enum.SortDirection.Asc"),
        SortDirection.Descending => new LocalizedData<SortDirection>(direction, "Enum.SortDirection.Desc"),
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, error_message(nameof(SortDirection)))
    };

    public static LocalizedData<AnimeSearchOrderBy> LocalizeWithData(this AnimeSearchOrderBy order) => order switch
    {
        AnimeSearchOrderBy.NoSorting => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.NoSorting"),
        AnimeSearchOrderBy.Title => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.Title"),
        AnimeSearchOrderBy.Score => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.Score"),
        AnimeSearchOrderBy.Type => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.Type"),
        AnimeSearchOrderBy.ScoredBy => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.ScoredBy"),
        AnimeSearchOrderBy.Members => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.Members"),
        AnimeSearchOrderBy.Favorites => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.Favorites"),
        AnimeSearchOrderBy.Id => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.Id"),
        AnimeSearchOrderBy.Episodes => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.Episodes"),
        AnimeSearchOrderBy.Rating => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.Rating"),
        AnimeSearchOrderBy.StartDate => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.StartDate"),
        AnimeSearchOrderBy.EndDate => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.EndDate"),
        AnimeSearchOrderBy.Rank => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.Rank"),
        AnimeSearchOrderBy.Popularity => new LocalizedData<AnimeSearchOrderBy>(order, "Enum.AnimeSearchOrderBy.Popularity"),
        _ => throw new ArgumentOutOfRangeException(nameof(order), order, error_message(nameof(AnimeSearchOrderBy)))
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
        _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, error_message(nameof(AnimeTypeFilter)))
    };

    public static LocalizedData<AiringStatusFilter> LocalizeWithData(this AiringStatusFilter filter) => filter switch
    {
        AiringStatusFilter.All => new LocalizedData<AiringStatusFilter>(filter, "Label.All"),
        AiringStatusFilter.Airing => new LocalizedData<AiringStatusFilter>(filter, "Enum.AiringStatus.Airing"),
        AiringStatusFilter.Complete => new LocalizedData<AiringStatusFilter>(filter, "Enum.AiringStatus.Complete"),
        AiringStatusFilter.Upcoming => new LocalizedData<AiringStatusFilter>(filter, "Enum.AiringStatus.Upcoming"),
        _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, error_message(nameof(AiringStatusFilter)))
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
        _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, error_message(nameof(AnimeAgeRatingFilter)))
    };

    public static LocalizedString LocalizeEpisodeNumber(this AnimeEpisodeResponse response)
    {
        return new LocalizedString("Label.Episode.Number", response.MalId);
    }

    private static string error_message(string paramName) => $"The passed {paramName} was invalid";
}
