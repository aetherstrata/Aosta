using System.Globalization;

using Aosta.Data.Models.Embedded;
using Aosta.Data.Enums;
using Aosta.Data.Models;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Response;

using Riok.Mapperly.Abstractions;

using AiringStatus = Aosta.Data.Enums.AiringStatus;

namespace Aosta.Data.Mapper;

[Mapper]
public static partial class JikanMapper
{
    [MapProperty(nameof(AnimeResponse.MalId), nameof(Anime.ID))]
    [MapProperty(nameof(AnimeResponse.Status), nameof(Anime.AiringStatus))]
    public static partial Anime ToModel(this AnimeResponse source);

    [MapProperty(nameof(AnimeResponse.MalId), nameof(Anime.ID))]
    [MapProperty(nameof(AnimeResponse.Status), nameof(Anime.AiringStatus))]
    public static partial Anime ToModel(this AnimeResponseFull source);

    public static Episode ToModel(this AnimeEpisodeResponse response)
    {
        var target = response.toModel();

        target.Titles.Add(new TitleEntry(TitleEntry.DEFAULT_KEY, response.Title));

        if (response.TitleJapanese is not null)
        {
            target.Titles.Add(new TitleEntry(TitleEntry.JAPANESE_KEY, response.TitleJapanese));
        }

        if (response.TitleRomanji is not null)
        {
            target.Titles.Add(new TitleEntry(TitleEntry.ROMANJI_KEY, response.TitleRomanji));
        }

        return target;
    }

    internal static AnimeBroadcast ToModel(this AnimeBroadcastResponse source)
    {
        var target = new AnimeBroadcast();
        if (source.Time != null)
        {
            var zoneinfo = TimeZoneInfo.FindSystemTimeZoneById(source.Timezone ?? "UTC");
            target.Time = DateTimeOffset
                .Parse(source.Time, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal)
                .Subtract(zoneinfo.BaseUtcOffset)
                .ToOffset(zoneinfo.BaseUtcOffset);
        }
        target.Day = source.Day;
        return target;
    }

    internal static partial ImagesSet ToModel(this ImagesSetResponse source);
    internal static partial Image ToModel(this ImageResponse source);
    internal static partial TimePeriod ToModel(this TimePeriodResponse source);
    internal static partial MalUrl ToModel(this MalUrlResponse source);
    internal static partial TitleEntry ToModel(this TitleEntryResponse source);
    internal static partial YouTubeVideo ToModel(this AnimeTrailerResponse source);

    private static partial Episode toModel(this AnimeEpisodeResponse source);

    private static AudienceRating toLocalType(this string str) => str switch
    {
        "G - All Ages" => AudienceRating.Everyone,
        "PG - Children" => AudienceRating.Children,
        "PG-13 - Teens 13 or older" => AudienceRating.Teens,
        "R - 17+ (violence & profanity)" => AudienceRating.ViolenceProfanity,
        "R+ - Mild Nudity" => AudienceRating.MildNudity,
        "Rx - Hentai" => AudienceRating.Hentai,
        _ => throw new ArgumentOutOfRangeException(nameof(str), str, $"The string did not have a valid conversion to {nameof(AudienceRating)}!")
    };

    private static ContentType toLocalType(this AnimeType type) => type switch
    {
        AnimeType.TV => ContentType.TV,
        AnimeType.TVSpecial => ContentType.TVSpecial,
        AnimeType.Special => ContentType.Special,
        AnimeType.OVA => ContentType.OVA,
        AnimeType.ONA => ContentType.ONA,
        AnimeType.Movie => ContentType.Movie,
        AnimeType.Music => ContentType.Music,
        AnimeType.PromotionalVideo => ContentType.PromotionalVideo,
        AnimeType.CommercialMessage => ContentType.CommercialMessage,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"{nameof(AnimeType)} did not have a valid value!")
    };

    private static AiringStatus toLocalType(this Aosta.Jikan.Enums.AiringStatus status) => status switch
    {
        Jikan.Enums.AiringStatus.Airing => AiringStatus.CurrentlyAiring,
        Jikan.Enums.AiringStatus.Completed => AiringStatus.FinishedAiring,
        Jikan.Enums.AiringStatus.Upcoming => AiringStatus.NotYetAired,
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, $"{nameof(Aosta.Jikan.Enums.AiringStatus)} did not have a valid value!")
    };
}
