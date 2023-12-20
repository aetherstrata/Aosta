// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.Threading;

using Aosta.Core.Database;

using DynamicData;
using DynamicData.Binding;

using Realms;

namespace Aosta.Ava.Extensions;

public static class RealmExtensions
{
    private static readonly ThreadLocal<Realm> s_UpdateRealm = new();

    public static IObservable<IChangeSet<T>> Connect<T>(this RealmAccess access) where T : IRealmObject
    {
        if (!s_UpdateRealm.IsValueCreated)
        {
            s_UpdateRealm.Value = access.GetRealm();
        }

        Debug.Assert(s_UpdateRealm.Value != null, "updateRealm.Value != null");

        return s_UpdateRealm.Value.All<T>().AsRealmCollection().ToObservableChangeSet<IRealmCollection<T>, T>();
    }
}
