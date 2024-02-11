// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Aosta.Ava.Extensions;
using Aosta.Data;
using Aosta.Data.Extensions;

using Splat;

namespace Aosta.Ava.Settings;

public static class Setting
{
    private static readonly Lazy<RealmAccess> lazy_realm = new(() => Locator.Current.GetSafely<RealmAccess>());

    private const string include_nsfw = "IncludeNsfw";
    private const string include_unapproved = "IncludeUnapproved";

    public static bool IncludeNsfw
    {
        get => !lazy_realm.Value.GetSetting(include_nsfw, false);
        set => lazy_realm.Value.SetSetting(include_nsfw, value);
    }

    public static bool IncludeUnapproved
    {
        get => !lazy_realm.Value.GetSetting(include_unapproved, false);
        set => lazy_realm.Value.SetSetting(include_unapproved, value);
    }
}
