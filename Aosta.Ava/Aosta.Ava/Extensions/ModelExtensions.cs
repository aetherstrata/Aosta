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
        AiringStatus.NotAvailable => "Label.NotAvailable.Long".Localize(),
        AiringStatus.Cancelled => "Enum.AiringStatus.Cancelled".Localize(),
        AiringStatus.FinishedAiring => "Enum.AiringStatus.Finished".Localize(),
        AiringStatus.NotYetAired => "Enum.AiringStatus.Upcoming".Localize(),
        AiringStatus.CurrentlyAiring => "Enum.AiringStatus.Airing".Localize(),
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, error_message(nameof(AiringStatus)))
    };

    public static LocalizedString Localize(this ContentType type) => type switch
    {
        ContentType.NotAvailable => "Label.NotAvailable.Long".Localize(),
        ContentType.CommercialMessage => "Enum.AnimeType.CM".Localize(),
        ContentType.Movie => "Enum.AnimeType.Movie".Localize(),
        ContentType.Music => "Enum.AnimeType.Music".Localize(),
        ContentType.ONA => "Enum.AnimeType.ONA".Localize(),
        ContentType.OVA => "Enum.AnimeType.OVA".Localize(),
        ContentType.PromotionalVideo => "Enum.AnimeType.PV".Localize(),
        ContentType.Special => "Enum.AnimeType.Special".Localize(),
        ContentType.TV => "Enum.AnimeType.TV".Localize(),
        ContentType.TVSpecial => "Enum.AnimeType.TVSpecial".Localize(),
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, error_message(nameof(ContentType)))
    };

    public static LocalizedString Localize(this ContentType? e) => e switch
    {
        not null => Localize(e.Value),
        null => LocalizedString.NA
    };

    public static LocalizedString Localize(this Season e) => e switch
    {
        Season.Winter => "Enum.Season.Winter".Localize(),
        Season.Spring => "Enum.Season.Spring".Localize(),
        Season.Summer => "Enum.Season.Summer".Localize(),
        Season.Fall => "Enum.Season.Fall".Localize(),
        _ => throw new ArgumentOutOfRangeException(nameof(e), e, error_message(nameof(Season)))
    };

    public static LocalizedString Localize(this Season? e) => e switch
    {
        not null => Localize(e.Value),
        null => LocalizedString.NA
    };

    private static string error_message(string paramName) => $"The passed {paramName} was invalid";
}
