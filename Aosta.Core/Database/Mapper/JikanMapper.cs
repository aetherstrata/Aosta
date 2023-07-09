using System.Globalization;
using Aosta.Core.Database.Models.Embedded;
using Aosta.Core.Database.Models.Jikan;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Response;
using Riok.Mapperly.Abstractions;

namespace Aosta.Core.Database.Mapper;

[Mapper]
public static partial class JikanMapper
{
    private static readonly IFormatProvider _formatProvider = new CultureInfo("en-US");

    public static partial JikanAnime ToJikanAnime(this AnimeResponse source);

    internal static AnimeBroadcast ToRealmModel(this AnimeBroadcastResponse source)
    {
        var target = new AnimeBroadcast();
        if (source.Time != null)
        {
            var zoneinfo = TimeZoneInfo.FindSystemTimeZoneById(source.Timezone ?? "UTC");
            target.Time = DateTimeOffset
                .Parse(source.Time, _formatProvider, DateTimeStyles.AssumeUniversal)
                .Subtract(zoneinfo.BaseUtcOffset)
                .ToOffset(zoneinfo.BaseUtcOffset);
        }
        target.Day = source.Day ?? DaysOfWeek.Unknown;
        target.String = source.String;
        return target;
    }

    internal static partial ImagesSet ToRealmModel(this ImagesSetResponse source);
    internal static partial Image ToRealmModel(this ImageResponse source);
    internal static partial TimePeriod ToRealmModel(this TimePeriodResponse source);
    internal static partial MalUrl ToRealmModel(this MalUrlResponse source);
    internal static partial TitleEntry ToRealmModel(this TitleEntryResponse source);
    internal static partial YouTubeVideo ToRealmModel(this AnimeTrailerResponse source);
}