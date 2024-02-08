using System.Globalization;

using Aosta.Data.Database.Enums;
using Aosta.Data.Database.Models;
using Aosta.Data.Database.Models.Embedded;
using Aosta.Data.Database.Models.Jikan;
using Aosta.Data.Database.Models.Local;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Response;

using Riok.Mapperly.Abstractions;

namespace Aosta.Data.Database.Mapper;

[Mapper]
public static partial class JikanMapper
{
    [MapProperty(nameof(AnimeResponse.MalId), nameof(JikanAnime.ID))]
    public static partial JikanAnime ToModel(this AnimeResponse source);

    [MapProperty(nameof(AnimeResponse.MalId), nameof(JikanAnime.ID))]
    public static partial JikanAnime ToModel(this AnimeResponseFull source);

    public static partial JikanEpisode ToModel(this AnimeEpisodeResponse source);

    /// <summary>
    /// Create a new <see cref="Anime"/> object with this Jikan model
    /// </summary>
    /// <param name="source">The Jikan model</param>
    /// <returns>A new <see cref="Anime"/> constructed with the given response</returns>
    public static Anime NewRecord(this JikanAnime source)
    {
        return new Anime
        {
            Jikan = source,
            Local = new LocalAnime()
        };
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

    private static ContentType toLocalType(this AnimeType type) => type switch
    {
        AnimeType.TV => ContentType.TV,
        AnimeType.TVSpecial => ContentType.Special,
        AnimeType.Special => ContentType.Special,
        AnimeType.OVA => ContentType.OVA,
        AnimeType.ONA => ContentType.ONA,
        AnimeType.Movie => ContentType.Movie,
        AnimeType.Music => ContentType.Music,
        AnimeType.PromotionalVideo => ContentType.Promotional,
        AnimeType.CommercialMessage => ContentType.Promotional,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
}
