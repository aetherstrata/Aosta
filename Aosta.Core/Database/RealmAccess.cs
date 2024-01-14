// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Core.Extensions;

using Realms;

using Serilog;

namespace Aosta.Core.Database;

/// <summary>
/// Use this class to interact with the realm. It acts as an access layer.
/// </summary>
/// <remarks>Prefer the methods offered by this class for one-time operations, get a Realm instance only when working with UI.</remarks>
public sealed class RealmAccess(ILogger log, RealmConfigurationBase config)
{
    //Most of the realm access code is taken from ppy/osu of which does extensive use for their game

    /// <summary>
    /// Run work on realm with a return value.
    /// </summary>
    /// <param name="action">The work to run.</param>
    /// <typeparam name="T">The return type.</typeparam>
    public T Run<T>(Func<Realm, T> action)
    {
        using var realm = GetRealm();

        T res = action(realm);

        log.Debug("Performed an action on the database. Returning its output");

        return res;
    }

    /// <summary>
    /// Run work on realm.
    /// </summary>
    /// <param name="action">The work to run.</param>
    public void Run(Action<Realm> action)
    {
        using var realm = GetRealm();

        action(realm);

        log.Debug("Performed an action on the database");
    }

    /// <summary>
    /// Execute a write procedure on realm.
    /// </summary>
    /// <param name="action">The work to run inside the realm's scope.</param>
    public void Write(Action<Realm> action)
    {
        using var realm = GetRealm();

        realm.Write(action);

        log.Debug("Performed a write operation on the database");
    }

    /// <summary>
    /// Execute a write procedure on realm.
    /// </summary>
    /// <param name="func">The work to run inside the realm's scope.</param>
    /// <typeparam name="T">Realm object type</typeparam>
    /// <returns>The instance of the written object</returns>
    public T Write<T>(Func<Realm, T> func) where T : IRealmObject
    {
        using var realm = GetRealm();

        T res = realm.Write(func);

        log.Debug("Performed a write operation on the database. Returning its output");

        return res;
    }

    private readonly CountdownEvent _pendingAsyncWrites = new(0);

    /// <summary>
    /// Write changes to realm asynchronously, guaranteeing order of execution.
    /// </summary>
    /// <param name="action">The work to run.</param>
    public Task WriteAsync(Action<Realm> action)
    {
        // CountdownEvent will fail if already at zero.
        if (!_pendingAsyncWrites.TryAddCount())
            _pendingAsyncWrites.Reset(1);

        // Regardless of calling Realm.GetInstance or Realm.GetInstanceAsync, there is a blocking overhead on retrieval.
        // Adding a forced Task.Run resolves this.
        return Task.Run(async () =>
        {
            using (var realm = GetRealm())
                // ReSharper disable once AccessToDisposedClosure (WriteAsync should be marked as [InstantHandle]).
                await realm.WriteAsync(() => action(realm)).ConfigureAwait(false);

            log.Debug("Performed an async write to the database. Emitting signal...");

            _pendingAsyncWrites.Signal();
        });
    }

    /// <summary>
    /// Check if Realm contains an object with a specific MAL ID
    /// </summary>
    /// <param name="malId">The MyAnimeList ID of the anime</param>
    /// <returns><c>true</c> if an object with that key is present, <c>false</c> otherwise</returns>
    public bool Exists<TEntity>(long malId) where TEntity : IRealmObject
    {
        using var realm = GetRealm();
        return realm.Find<TEntity>(malId) != null;
    }

    public void Delete()
    {
        Realm.DeleteRealm(config);
    }

    public Realm GetRealm()
    {
        return Realm.GetInstance(config);
    }
}
