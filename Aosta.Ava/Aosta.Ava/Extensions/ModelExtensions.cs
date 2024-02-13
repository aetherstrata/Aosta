// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Aosta.Ava.Localization;
using Aosta.Data.Enums;

namespace Aosta.Ava.Extensions;

public static class ModelExtensions
{
    public static LocalizedString Localize(this AiringStatus status) => status switch
    {
        AiringStatus.NotAvailable => new LocalizedString("Label.NotAvailable.Long"),
        AiringStatus.Cancelled => new LocalizedString("Enum.AiringStatus.Cancelled"),
        AiringStatus.FinishedAiring => new LocalizedString("Enum.AiringStatus.Finished"),
        AiringStatus.NotYetAired => new LocalizedString("Enum.AiringStatus.Upcoming"),
        AiringStatus.CurrentlyAiring => new LocalizedString("Enum.AiringStatus.Airing"),
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, error_message(nameof(AiringStatus)))
    };

    public static LocalizedString Localize(this ContentType type) => type switch
    {
        ContentType.NotAvailable => new LocalizedString("Label.NotAvailable.Long"),
        ContentType.CommercialMessage => new LocalizedString("Enum.AnimeType.CM"),
        ContentType.Movie => new LocalizedString("Enum.AnimeType.Movie"),
        ContentType.Music => new LocalizedString("Enum.AnimeType.Music"),
        ContentType.ONA => new LocalizedString("Enum.AnimeType.ONA"),
        ContentType.OVA => new LocalizedString("Enum.AnimeType.OVA"),
        ContentType.PromotionalVideo => new LocalizedString("Enum.AnimeType.PV"),
        ContentType.Special => new LocalizedString("Enum.AnimeType.Special"),
        ContentType.TV => new LocalizedString("Enum.AnimeType.TV"),
        ContentType.TVSpecial => new LocalizedString("Enum.AnimeType.TVSpecial"),
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, error_message(nameof(ContentType)))
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
        _ => throw new ArgumentOutOfRangeException(nameof(e), e, error_message(nameof(Season)))
    };

    public static LocalizedString Localize(this Season? e) => e switch
    {
        not null => Localize(e.Value),
        null => LocalizedString.NA
    };

    private static string error_message(string paramName) => $"The passed {paramName} was invalid";
}
