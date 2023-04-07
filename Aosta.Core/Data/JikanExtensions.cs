using Aosta.Core.Data.Models;
using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Jikan.Models.Response;

namespace Aosta.Core.Data;

internal static class JikanExtensions
{
    internal static JikanContentObject ToRealmObject(this AnimeResponse animeResponse) => new(animeResponse);

    internal static BroadcastObject ToRealmObject(this AnimeBroadcastResponse broadcastResponse) => new(broadcastResponse);

    internal static ImageObject ToRealmObject(this ImageResponse imageResponse) => new(imageResponse);

    internal static ImageSetObject ToRealmObject(this ImagesSetResponse setResponse) => new(setResponse);

    internal static TimePeriodObject ToRealmObject(this TimePeriodResponse periodResponse) => new(periodResponse);

    internal static TitleObject ToRealmObject(this TitleEntryResponse title) => new(title);

    internal static TrailerObject ToRealmObject(this AnimeTrailerResponse trailerResponse) => new(trailerResponse);

    internal static UrlObject ToRealmObject(this MalUrlResponse urlResponse) => new(urlResponse);

    internal static void AddRange(this IList<UrlObject> list, ICollection<MalUrlResponse>? collection)
    {
        if (collection is null) return;

        foreach (var elem in collection)
        {
            list.Add(elem.ToRealmObject());
        }
    }

    internal static void AddRange(this IList<TitleObject> list, ICollection<TitleEntryResponse>? collection)
    {
        if (collection is null) return;

        foreach (var elem in collection)
        {
            list.Add(elem.ToRealmObject());
        }
    }
}