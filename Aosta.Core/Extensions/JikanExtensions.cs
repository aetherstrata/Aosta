using Aosta.Core.Data.Models;
using Aosta.Core.Data.Models.Jikan;
using JikanDotNet;

namespace Aosta.Core.Extensions;

internal static class JikanExtensions
{
    internal static JikanContentObject ToRealmObject(this Anime anime) => new(anime);

    internal static BroadcastObject ToRealmObject(this AnimeBroadcast broadcast) => new(broadcast);

    internal static ImageObject ToRealmObject(this Image image) => new(image);

    internal static ImageSetObject ToRealmObject(this ImagesSet set) => new(set);

    internal static TimePeriodObject ToRealmObject(this TimePeriod period) => new(period);

    internal static TitleObject ToRealmObject(this TitleEntry title) => new(title);

    internal static TrailerObject ToRealmObject(this AnimeTrailer trailer) => new(trailer);

    internal static UrlObject ToRealmObject(this MalUrl url) => new(url);

    internal static void AddRange(this IList<UrlObject> list, ICollection<MalUrl> collection)
    {
        foreach (var elem in collection)
        {
            list.Add(elem.ToRealmObject());
        }
    }

    internal static void AddRange(this IList<TitleObject> list, ICollection<TitleEntry> collection)
    {
        foreach (var elem in collection)
        {
            list.Add(elem.ToRealmObject());
        }
    }
}