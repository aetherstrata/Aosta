// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;

using Aosta.Core.Database;

using DynamicData;

using Realms;

using Splat;

namespace Aosta.Ava.Extensions;

public static class RealmExtensions
{
    private static readonly Serilog.ILogger s_Logger = Locator.Current.GetSafely<Serilog.ILogger>();

    public static IObservable<IChangeSet<T,TKey>> Connect<T, TKey>(this IQueryable<T> query)
        where T : IRealmObject, IHasPrimaryKey<TKey>
        where TKey : struct
    {
        var cache = new SourceCache<T, TKey>(x => x.ID);

        query.SubscribeForNotifications((sender, changes) =>
        {
            s_Logger.Verbose("Projection for {Type} changed, adding changes to observable cache", typeof(T));

            if (changes is null) return;

            cache.AddOrUpdate(sender);
        });

        return cache.Connect();
    }
}
