// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Data.Extensions;

using Realms;
using Realms.Logging;

using Serilog;

namespace Aosta.Data;

/// <summary>
/// Use this class to interact with the <see cref="Realm"/>.
/// <br/><br/>
/// Prefer the methods offered by this class for one-time operations, get a <see cref="Realm"/> instance only when working with UI.
/// </summary>
/// <remarks>
/// This does not redirect <see cref="Realm"/>'s internal logs to Serilog. If needed, set <see cref="Logger.Default">Logger.Default</see> accordingly with a <see cref="Logger.Function(System.Action{Realms.Logging.LogLevel,string})">Logger.Function</see>
/// </remarks>
public sealed class RealmAccess : IEquatable<RealmAccess>
{
    private readonly ILogger? _log;
    private readonly RealmConfigurationBase _config;

    public RealmAccess(string realmPath, ILogger? log = null)
    {
        _log = log;

        _log?.Information("Initializing Realm at {Path}", realmPath);

        _config = new RealmConfiguration(realmPath)
        {
            SchemaVersion = 2,
            IsReadOnly = false,
#if DEBUG
            ShouldDeleteIfMigrationNeeded = true
#else
            ShouldDeleteIfMigrationNeeded = false
#endif
        };
    }

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

        _log?.Debug("Performed an action on the database. Returning its output");

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

        _log?.Debug("Performed an action on the database");
    }

    /// <summary>
    /// Execute a write procedure on realm.
    /// </summary>
    /// <param name="action">The work to run inside the realm's scope.</param>
    public void Write(Action<Realm> action)
    {
        using var realm = GetRealm();

        realm.Write(action);

        _log?.Debug("Performed a write operation on the database");
    }

    /// <summary>
    /// Execute a write procedure to <see cref="Realm"/>.
    /// </summary>
    /// <param name="func">The work to run inside the realm's scope.</param>
    public T Write<T>(Func<Realm, T> func)
    {
        using var realm = GetRealm();

        T res = realm.Write(func);

        _log?.Debug("Performed a write operation on the database. Returning its output");

        return res;
    }

    private readonly CountdownEvent _pendingAsyncWrites = new(0);

    /// <summary>
    /// Write changes to realm asynchronously, guaranteeing order of execution.
    /// </summary>
    /// <param name="action">The work to run.</param>
    /// <param name="ct">The cancellation token.</param>
    public Task WriteAsync(Action<Realm> action, CancellationToken ct = default)
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
                await realm.WriteAsync(() => action(realm), ct)
                    .ConfigureAwait(false);

            _log?.Debug("Performed an async write to the database. Emitting signal...");

            _pendingAsyncWrites.Signal();
        }, ct);
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
        Realm.DeleteRealm(_config);
    }

    public Realm GetRealm()
    {
        return Realm.GetInstance(_config);
    }

    #region Equality Methods

    public bool Equals(RealmAccess? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _config.Equals(other._config);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is RealmAccess other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _config.GetHashCode();
    }

    #endregion
}
