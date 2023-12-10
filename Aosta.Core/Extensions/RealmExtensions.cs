using Realms;

namespace Aosta.Core.Data;

public static class RealmExtensions
{
    /// <summary>
    /// Shorthand for calling All().First() on a Realm database
    /// </summary>
    /// <param name="realm">The realm to operate on.</param>
    /// <typeparam name="T">The type of the object to query.</typeparam>
    /// <returns></returns>
    public static T First<T>(this Realm realm) where T : IRealmObject
    {
        return realm.All<T>().First();
    }

    /// <summary>
    /// Perform a write operation against the provided realm instance.
    /// </summary>
    /// <remarks>
    /// This will automatically start a transaction if not already in one.
    /// </remarks>
    /// <param name="realm">The realm to operate on.</param>
    /// <param name="function">The write operation to run.</param>
    public static void Write(this Realm realm, Action<Realm> function)
    {
        Transaction? transaction = null;

        try
        {
            if (!realm.IsInTransaction)
                transaction = realm.BeginWrite();

            function(realm);

            transaction?.Commit();
        }
        finally
        {
            transaction?.Dispose();
        }
    }

    /// <summary>
    /// Perform a write operation against the provided realm instance.
    /// </summary>
    /// <remarks>
    /// This will automatically start a transaction if not already in one.
    /// </remarks>
    /// <param name="realm">The realm to operate on.</param>
    /// <param name="function">The write operation to run.</param>
    public static T Write<T>(this Realm realm, Func<Realm, T> function)
    {
        Transaction? transaction = null;

        try
        {
            if (!realm.IsInTransaction)
                transaction = realm.BeginWrite();

            var result = function(realm);

            transaction?.Commit();

            return result;
        }
        finally
        {
            transaction?.Dispose();
        }
    }

    /* TODO: fix after models
    public static IOrderedQueryable<Anime> OrderBy(this IQueryable<Anime> query, AnimeOrdering ordering)
    {
        return ordering switch
        {
            AnimeOrdering.ByTitle => query.OrderBy(x => x.Title).ThenBy(x => x.Id),
            AnimeOrdering.ByScore => query.OrderBy(x => x.Score).ThenBy(x => x.Id),
            AnimeOrdering.ByYear => query.OrderBy(x => x.Year).ThenBy(x => x.Id),
            AnimeOrdering.ByAiringStatus => query.OrderBy(x => x._AiringStatus).ThenBy(x => x.Id),
            AnimeOrdering.ByWatchStatus => query.OrderByDescending(x => x._WatchStatus).ThenBy(x => x.Id),
            _ => query.OrderBy(x => x.Id)
        };
    }
    */
}
