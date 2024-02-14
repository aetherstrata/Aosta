using System.Linq.Expressions;

using Aosta.Common.Extensions;
using Aosta.Data.Database.Models;

using Realms;

namespace Aosta.Data.Extensions;

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

    public static IQueryable<TEntity> Is<TEntity>(this IQueryable<TEntity> source, string propertyName, QueryArgument value)
        where TEntity : IRealmObject
    {
        string query = $"{propertyName} == $0";
        return source.Filter(query, value);
    }

    public static IQueryable<TEntity> Is<TEntity>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, object?>> lambda,
        QueryArgument value)
        where TEntity : IRealmObject
    {
        string property = lambda.GetPropertyName();

        if (string.IsNullOrEmpty(property))
        {
            throw new ArgumentException("This is not a valid member expression", nameof(lambda));
        }

        return source.Is(property, value);
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
    public static IQueryable<TEntity> In<TEntity, TArgument>(
        this IQueryable<TEntity> source,
        string propertyName,
        IEnumerable<TArgument> objList)
        where TEntity : IRealmObject
    {
        string query = $"{propertyName} IN {{{string.Join(',', objList)}}}";
        return source.Filter(query);
    }

    /// <summary>
    /// Filter a projection and keep only the elements that are contained in the argument list
    /// </summary>
    /// <param name="source">The projection to filter.</param>
    /// <param name="lambda">The property to filter on.</param>
    /// <param name="objList">The allowed values.</param>
    /// <typeparam name="TEntity">The type of the entities.</typeparam>
    /// <typeparam name="TArgument">The type of the arguments.</typeparam>
    /// <returns>The updated projection.</returns>
    /// <exception cref="ArgumentException">the provided property expression is invalid</exception>
    public static IQueryable<TEntity> In<TEntity, TArgument>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, object?>> lambda,
        IEnumerable<TArgument> objList)
        where TEntity : IRealmObject
    {
        string property = lambda.GetPropertyName();

        if (string.IsNullOrEmpty(property))
        {
            throw new ArgumentException("This is not a valid member expression", nameof(lambda));
        }

        return source.In(property, objList);
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

    /// <summary>
    /// Get the setting value of the given key.
    /// </summary>
    /// <param name="realm">The realm accessor to perform the operation on.</param>
    /// <param name="key">The setting key.</param>
    /// <param name="fallback">The default value.</param>
    /// <typeparam name="T">The type of the setting.</typeparam>
    /// <returns>The setting value or <c>default(<typeparamref name="T">T</typeparamref>)</c> if not found.</returns>
    /// <exception cref="InvalidCastException">The underlying <see cref="RealmValue"/> could not be cast to <typeparamref name="T"/>.</exception>
    public static T GetSetting<T>(this RealmAccess realm, string key, T fallback)
    {
        realm.Log?.Debug("Getting setting value for {Key}", key);

        return realm.Run(r =>
        {
            var setting = r.Find<Setting>(key);
            return setting is null ? fallback : setting.Value.As<T>();
        });
    }

    /// <summary>
    /// Get the setting value of a given key.
    /// </summary>
    /// <param name="realm">The realm to perform the operation on.</param>
    /// <param name="key">The setting key.</param>
    /// <param name="fallback">The default value.</param>
    /// <param name="field">The field to write the setting value onto.</param>
    /// <typeparam name="T">The type of the setting.</typeparam>
    /// <returns>The realm accessor for method chaining.</returns>
    /// <exception cref="InvalidCastException">The <see cref="RealmValue"/> could not be cast to the given type.</exception>
    public static RealmAccess GetSetting<T>(this RealmAccess realm, string key, T fallback, out T field)
    {
        field = realm.GetSetting(key, fallback);

        return realm;
    }

    /// <summary>
    /// Get the setting value of a given key.
    /// </summary>
    /// <param name="realm">The realm to perform the operation on.</param>
    /// <param name="key">The setting key.</param>
    /// <param name="value">The value to set.</param>
    /// <returns>the realm accessor for method chaining.</returns>
    public static RealmAccess SetSetting(this RealmAccess realm, string key, RealmValue value)
    {
        realm.Run(r =>
        {
            var setting = r.Find<Setting>(key);

            if (setting is null) r.Write(() => r.Add(new Setting(key, value)));

            else r.Write(() => setting.Value = value);
        });

        realm.Log?.Debug("Saved setting value for {Key} => {NewValue}", key, value);

        return realm;
    }
}
