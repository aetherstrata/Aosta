using Aosta.Core.Data;
using Aosta.Core.Database.Models;

using Realms;
using Serilog;

namespace Aosta.Core;

/// <summary>
/// Facade class that exposes core functionality to consumer applications
/// </summary>
public class AostaDotNet
{
    /// <summary> Serilog logger instance </summary>
    public required ILogger Log { get; init; }

    /// <summary> Realm configuration </summary>
    public required RealmConfiguration RealmConfig { get; init; }

    /// <summary> Explicit parameterless constructor internal to forbid direct initialization </summary>
    internal AostaDotNet()
    {
    }

    internal void Initialize()
    {
        using var realm = getRealm();

        if (realm.Find<User>(Guid.Empty) is null)
        {
            Log?.Warning("Local user not found. Creating new user on database");

            Write(r =>
            {
                r.Add(User.Empty());
            });
        }

        Log?.Information("Aosta corelib initialized");
    }

    //Most of the realm access code is taken from ppy/osu which does extensive use of realm for their game

    /// <summary>
    /// Run work on realm with a return value.
    /// </summary>
    /// <param name="action">The work to run.</param>
    /// <typeparam name="T">The return type.</typeparam>
    public T Run<T>(Func<Realm, T> action)
    {
        using var realm = getRealm();

        T res =  action(realm);

        Log.Debug("Performed an action on the database. Returning its output");

        return res;
    }

    /// <summary>
    /// Run work on realm.
    /// </summary>
    /// <param name="action">The work to run.</param>
    public void Run(Action<Realm> action)
    {
        using var realm = getRealm();

        action(realm);

        Log.Debug("Performed an action on the database");
    }

    public void Write(Action<Realm> action)
    {
        using var realm = getRealm();

        realm.Write(action);

        Log.Debug("Performed a write operation on the database");
    }

    public T Write<T>(Func<Realm, T> func) where T : IRealmObject
    {
        using var realm = getRealm();

        T res =  realm.Write(func);

        Log.Debug("Performed a write operation on the database. Returning its output");

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
            using (var realm = getRealm())
                // ReSharper disable once AccessToDisposedClosure (WriteAsync should be marked as [InstantHandle]).
                await realm.WriteAsync(() => action(realm)).ConfigureAwait(false);

            Log.Debug("Performed an async write to the database. Emitting signal...");

            _pendingAsyncWrites.Signal();
        });
    }

    public User GetLocalUser()
    {
        using var realm = getRealm();

        if (realm.Find<User>(Guid.Empty) is { } localUser)
        {
            return localUser;
        }

        var ex = new InvalidOperationException("Could not find local user");
        Log.Error(ex, "Could not find local user in database");
        throw ex;
    }

    /// <summary>
    /// Check if Realm contains an object with a specific MAL ID
    /// </summary>
    /// <param name="malId">The MyAnimeList ID of the anime</param>
    /// <returns><c>true</c> if an object with that key is present, <c>false</c> otherwise</returns>
    public bool Exists<TEntity>(long malId) where TEntity : IRealmObject
    {
        using var realm = getRealm();
        return realm.Find<TEntity>(malId) != null;
    }

    public void DeleteRealm()
    {
        Realm.DeleteRealm(RealmConfig);
    }

    private Realm getRealm()
    {
        return Realm.GetInstance(RealmConfig);
    }
}
