using Aosta.Core.Data.Models;
using Aosta.Core.Data.Ordering;
using Realms;

namespace Aosta.Core.Extensions;

public static class RealmExtensions
{
    public static T First<T>(this Realm realm) where T : IRealmObject
    {
        return realm.All<T>().First();
    }

    public static T? FirstOrDefault<T>(this Realm realm) where T : IRealmObject
    {
        return realm.All<T>().FirstOrDefault();
    }


    public static IOrderedQueryable<ContentObject> OrderBy(this IQueryable<ContentObject> query, AnimeOrdering ordering)
    {
        return ordering switch
        {
            AnimeOrdering.ByTitle => query.OrderBy(x => x.Title).ThenBy(x => x.JikanResponseData.DefaultTitle)
                .ThenBy(x => x.Id),
            AnimeOrdering.ByScore => query.OrderBy(x => x.Score).ThenBy(x => x.Id),
            AnimeOrdering.ByYear => query.OrderBy(x => x.Year).ThenBy(x => x.Id),
            AnimeOrdering.ByAiringStatus => query.OrderBy(x => x.AiringStatus).ThenBy(x => x.Id),
            AnimeOrdering.ByWatchStatus => query.OrderByDescending(x => x._WatchStatus).ThenBy(x => x.Id),
            _ => query.OrderBy(x => x.Id)
        };
    }
}