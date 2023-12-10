using System.Globalization;

using Aosta.Core.Database.Enums;
using Aosta.Core.Database.Models;
using Aosta.Core.Database.Models.Embedded;
using Aosta.Core.Database.Models.Jikan;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Response;

using Microsoft.Win32;

using Riok.Mapperly.Abstractions;

namespace Aosta.Core.Database.Mapper;

[Mapper]
public static partial class JikanMapper
{
    public static partial JikanAnime ToJikanAnime(this AnimeResponse source);
    public static partial JikanAnime ToJikanAnime(this AnimeResponseFull source);

    [MapperIgnoreTarget(nameof(Anime.Score))]
    public static partial Anime ToRealmModel(this JikanAnime source);

    internal static ContentType ToLocalType(this AnimeType type) => type switch
    {
        AnimeType.TV => ContentType.TV,
        AnimeType.OVA => ContentType.OVA,
        AnimeType.Movie => ContentType.Movie,
        AnimeType.Special => ContentType.Special,
        AnimeType.ONA => ContentType.ONA,
        AnimeType.Music => ContentType.Music,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };

    internal static AnimeBroadcast ToRealmModel(this AnimeBroadcastResponse source)
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

    internal static partial ImagesSet ToRealmModel(this ImagesSetResponse source);
    internal static partial Image ToRealmModel(this ImageResponse source);
    internal static partial TimePeriod ToRealmModel(this TimePeriodResponse source);
    internal static partial MalUrl ToRealmModel(this MalUrlResponse source);
    internal static partial TitleEntry ToRealmModel(this TitleEntryResponse source);
    internal static partial YouTubeVideo ToRealmModel(this AnimeTrailerResponse source);
}
