// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
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
    /// <returns>The <see cref="IChangeSet{TObject}"/> to observe on.</returns>
    public static IObservable<IChangeSet<T>> Connect<T>(this IQueryable<T> query, out IDisposable token)
        where T : IRealmObject
    {
        var cache = new SourceList<T>();

        token = query.SubscribeForNotifications((sender, changes) =>
        {
            logger.Debug("Projection for {Type} changed, adding changes to observable cache", typeof(T).Name);

            // This happens when the collection is queried for the first time.
            if (changes is null)
            {
                cache.AddRange(sender);
                logger.Debug("Initialized the {Type} observable cache with {Count} elements",
                    typeof(T).Name, sender.Count);
            }
            else
            {
                cache.Edit(update =>
                {
                    // Handle deleted elements
                    foreach (int i in changes.DeletedIndices)
                    {
                        update.RemoveAt(i);
                    }

                    // Handle inserted elements
                    foreach (int i in changes.InsertedIndices)
                    {
                        update.Insert(i, sender[i]);
                    }

                    logger.Debug("Processed {ChangesCount} changes for {Type} observable cache: [Removed: {Removed}, Added: {Added}, Moved: {Moved}]",
                        changes.DeletedIndices.Length + changes.InsertedIndices.Length,
                        typeof(T).Name,
                        changes.DeletedIndices.Length - changes.Moves.Length,
                        changes.InsertedIndices.Length - changes.Moves.Length,
                        changes.Moves.Length);
                });
            }
        });

        return cache.Connect();
    }
}
