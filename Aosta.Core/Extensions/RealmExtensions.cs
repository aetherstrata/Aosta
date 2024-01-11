using Realms;

namespace Aosta.Core.Data;

public static class RealmExtensions
{
    /// <summary>
    /// Shorthand for calling All().First() on a Realm database
    /// </summary>
    /// <param name="realm">The realm to operate on.</param>
    /// <typeparam name="T">The type of the object to query.</typeparam>
    /// <returns>The first element in the Realm.</returns>
    public static T First<T>(this Realm realm) where T : IRealmObject
    {
        return realm.All<T>().First();
    }

    /// <summary>
    /// Filter a projection and keep only the elements that are contained in the argument list
    /// </summary>
    /// <param name="source">The projection to filter.</param>
    /// <param name="propertyName">The property to filter on.</param>
    /// <param name="objList">The allowed values.</param>
    /// <typeparam name="TEntity">The type of the entities.</typeparam>
    /// <typeparam name="TArgument">The type of the arguments.</typeparam>
    /// <returns>The updated projection.</returns>
    public static IQueryable<TEntity> In<TEntity, TArgument>(this IQueryable<TEntity> source, string propertyName, IEnumerable<TArgument> objList)
        where TEntity : IRealmObject
    {
        string query = $"{propertyName} IN {{{string.Join(',', objList)}}}";
        return source.Filter(query);
    }

    /// <summary>
    /// Perform a write operation against the provided realm instance.
    /// </summary>
    /// <remarks>
    /// This will automatically start a transaction if not already in one.
    /// </remarks>
    /// <param name="realm">The realm to operate on.</param>
    /// <param name="function">The write operation to run.</param>
    internal static void Write(this Realm realm, Action<Realm> function)
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
    internal static T Write<T>(this Realm realm, Func<Realm, T> function)
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
}
