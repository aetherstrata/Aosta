// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Realms;

namespace Aosta.Core.Database.Models;

public partial class Setting : IRealmObject
{
    [PrimaryKey]
    public string Key { get; private set; } = null!;

    public RealmValue Value { get; set; }

    // Used internally by Realm
    private Setting() { }

    public Setting(string key, RealmValue value = default)
    {
        Key = key;
        Value = value;
    }
}
