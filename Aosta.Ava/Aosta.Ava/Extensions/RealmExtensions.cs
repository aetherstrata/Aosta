// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;

using DynamicData;

using Realms;

using Splat;

using ILogger = Serilog.ILogger;

namespace Aosta.Ava.Extensions;

public static class RealmExtensions
{
    private static readonly ILogger logger = Locator.Current.GetSafely<ILogger>();

    /// <summary>
    /// Convert the Realm projection into an observable list and subscribe to the collection changes.
    /// </summary>
    /// <param name="query">The Realm projection.</param>
    /// <param name="token">The returned subscription token.</param>
    /// <typeparam name="T">The type of the entities.</typeparam>
    /// <returns>The <see cref="IChangeSet{TObject}">IChangeSet</see> to observe on.</returns>
    public static IObservable<IChangeSet<T>> Connect<T>(this IQueryable<T> query, out IDisposable token)
        where T : IRealmObjectBase
    {
        return Connect(query.AsRealmCollection(), out token);
    }

    /// <summary>
    /// Convert the Realm list into an observable list and subscribe to the collection changes.
    /// </summary>
    /// <param name="list">The Realm list.</param>
    /// <param name="token">The returned subscription token.</param>
    /// <typeparam name="T">The type of the entities.</typeparam>
    /// <returns>The <see cref="IChangeSet{TObject}">IChangeSet</see> to observe on.</returns>
    public static IObservable<IChangeSet<T>> Connect<T>(this IList<T> list, out IDisposable token)
        where T : IRealmObjectBase
    {
        return Connect(list.AsRealmCollection(), out token);
    }

    /// <summary>
    /// Convert the Realm collection into an observable list and subscribe to the collection changes.
    /// </summary>
    /// <param name="collection">The Realm collection.</param>
    /// <param name="token">The returned subscription token.</param>
    /// <typeparam name="T">The type of the entities.</typeparam>
    /// <returns>The <see cref="IChangeSet{TObject}">IChangeSet</see> to observe on.</returns>
    public static IObservable<IChangeSet<T>> Connect<T>(this IRealmCollection<T> collection, out IDisposable token)
        where T : IRealmObjectBase
    {
        var cache = new SourceList<T>();

        token = collection.SubscribeForNotifications((sender, changes) =>
        {
            logger.Debug("Projection for {Type} changed, adding changes to observable cache", typeof(T).Name);

            // This happens when the collection is queried for the first time.
            if (changes is null)
            {
                cache.AddRange(sender);
                logger.Debug("Initialized the {Type} observable cache with {Count} elements",
                    typeof(T).Name, sender.Count);
            }
            else if (changes.IsCleared)
            {
                cache.Clear();
                logger.Debug("Observable cache for {Type} has been cleared", typeof(T).Name);
            }
            else
            {
                cache.Edit(update =>
                {
                    // Handle deleted elements
                    for (int index = 0; index < changes.DeletedIndices.Length; index++)
                    {
                        int i = changes.DeletedIndices[index] - index;
                        update.RemoveAt(i);
                    }

                    // Handle inserted elements
                    foreach (int i in changes.InsertedIndices)
                    {
                        update.Insert(i, sender[i]);
                    }
                });

                logger.Debug("Processed {ChangesCount} changes for {Type} observable cache: [Removed: {Removed}, Added: {Added}, Moved: {Moved}]",
                    changes.DeletedIndices.Length + changes.InsertedIndices.Length,
                    typeof(T).Name,
                    changes.DeletedIndices.Length - changes.Moves.Length,
                    changes.InsertedIndices.Length - changes.Moves.Length,
                    changes.Moves.Length);
            }
        });

        return cache.Connect();
    }
}
