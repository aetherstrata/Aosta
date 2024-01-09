// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;

using Aosta.Ava.Models;
using Aosta.Core.Database;

using DynamicData;

using Realms;

using Splat;

namespace Aosta.Ava.Extensions;

public static class RealmExtensions
{
    private static readonly Serilog.ILogger s_Logger = Locator.Current.GetSafely<Serilog.ILogger>();

    /// <summary>
    /// Convert the Realm projection into an observable list and subscribe to collection changes.
    /// </summary>
    /// <param name="query">The Realm projection.</param>
    /// <param name="token">The returned subscription token.</param>
    /// <typeparam name="T">The type of the entities.</typeparam>
    /// <returns>The <see cref="IChangeSet{TObject}"/> to observe on.</returns>
    public static IObservable<IChangeSet<T>> Connect<T>(this IQueryable<T> query, out IDisposable token)
        where T : IRealmObject
    {
        var cache = new SourceList<T>();

        token = query.SubscribeForNotifications((sender, changes) =>
        {
            s_Logger.Debug("Projection for {Type} changed, adding changes to observable cache", typeof(T).Name);

            if (changes is null)
            {
                if (cache.Count == sender.Count) return;

                // If this is the first call, the cache must be initialized.
                cache.AddRange(sender);
                s_Logger.Verbose("Initialized the {Type} observable cache with {Count} elements", typeof(T).Name,
                    sender.Count);

                return;
            }

            cache.Edit(update =>
            {
                // Handle deleted elements
                int deletes = changes.DeletedIndices
                    .Except(changes.Moves.Select(move => move.From))
                    .Aggregate(0, (acc, curr) =>
                    {
                        update.RemoveAt(curr);
                        return acc + 1;
                    });

                // Handle inserted elements
                int inserts = changes.InsertedIndices
                    .Except(changes.Moves.Select(move => move.To))
                    .Aggregate(0, (acc, curr) =>
                    {
                        update.Insert(curr, sender[curr]);
                        return acc + 1;
                    });

                // Handle moved elements
                foreach (var move in changes.Moves)
                {
                    update.Move(move.From, move.To);
                }

                s_Logger.Verbose("Processed {ChangesCount} changes for {Type} observable cache: [Removed: {Removed}, Added: {Added}, Moved: {Moved}]",
                    deletes + inserts + changes.Moves.Length, typeof(T).Name, deletes, inserts, changes.Moves.Length);
            });
        });

        return cache.Connect();
    }

    /// <summary>
    /// Get the setting value of the given key.
    /// </summary>
    /// <param name="realm">The realm accessor to perform the operation on.</param>
    /// <param name="key">The setting key.</param>
    /// <typeparam name="T">The type of the setting.</typeparam>
    /// <returns>The setting value or <c>default(<typeparamref name="T">T</typeparamref>)</c> if not found.</returns>
    /// <exception cref="InvalidCastException">The <see cref="RealmValue"/> could not be cast to <typeparamref name="T"/>.</exception>
    public static T? GetSetting<T>(this RealmAccess realm, string key)
    {
        var setting = realm.Run(r => r.Find<Setting>(key));

        return setting is null ? default : setting.Value.As<T>();
    }

    /// <summary>
    /// Get the setting value of a given key.
    /// </summary>
    /// <param name="realm">The realm to perform the operation on.</param>
    /// <param name="key">The setting key.</param>
    /// <param name="field">The field to write the setting value onto.</param>
    /// <param name="fallback">The default value.</param>
    /// <typeparam name="T">The type of the setting.</typeparam>
    /// <returns>The realm accessor for method chaining.</returns>
    /// <exception cref="InvalidCastException">The <see cref="RealmValue"/> could not be cast to the given type.</exception>
    public static RealmAccess GetSetting<T>(this RealmAccess realm, string key, out T field, T fallback)
    {
        field = realm.GetSetting<T>(key) ?? fallback;

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
        var setting = realm.Run(r => r.Find<Setting>(key));

        if (setting is null)
        {
            realm.Write(r => r.Add(new Setting(key, value)));
        }
        else
        {
            realm.Write(_ => setting.Value = value);
        }

        return realm;
    }
}
