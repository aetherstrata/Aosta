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

    public static IEnumerable<TOut> Select<TSource, TOut>(this IQueryable<TSource> source, Func<TSource, TOut> selector)
        where TSource : IRealmObject
    {
        if (selector is null) throw new ArgumentNullException(nameof(selector) ,"Selector function was null");

        var elems = source.AsRealmCollection();

        return Select(elems, selector);
    }

    public static IEnumerable<TOut> Select<TSource, TOut>(this IRealmCollection<TSource> source, Func<TSource, TOut> selector)
    {
        if (selector is null) throw new ArgumentNullException(nameof(selector) ,"Selector function was null");

        for (int index = 0; index < source.Count; index++)
        {
            var elem = source[index];
            yield return selector(elem);
        }
    }
}
