// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Linq;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data;
using Aosta.Data.Extensions;

using Splat;

namespace Aosta.Ava.Settings;

public sealed record LanguageKey : ISetting<LanguageKey>
{
    private const string setting_key = "AppLang";

    private static readonly FrozenDictionary<string, LanguageKey> lookup =
        Enum.GetValues<InterfaceLanguage>()
            .Select(lang => new LanguageKey(lang))
            .ToFrozenDictionary(x => x.Key);

    public static readonly LanguageKey DEFAULT = new(InterfaceLanguage.English);

    /// <summary>
    /// The setting key for this interface language on Realm.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// The human-readable language name.
    /// </summary>
    public string Name => Language.GetLanguageName();

    /// <summary>
    /// The language enum associated to this setting key.
    /// </summary>
    public InterfaceLanguage Language { get; }

    /// <summary>
    /// Get all available language options.
    /// </summary>
    /// <returns>The immutable array of the keys.</returns>
    public static ImmutableArray<LanguageKey> All() => lookup.Values;

    private LanguageKey(InterfaceLanguage lang)
    {
        Key = setting_key + lang.GetLanguageCode();
        Language = lang;
    }

    public static LanguageKey Load()
    {
        var realm = Locator.Current.GetSafely<RealmAccess>();

        string setting = realm.GetSetting(setting_key, DEFAULT.Key);

        return lookup[setting];
    }

    public void Save()
    {
        var realm = Locator.Current.GetSafely<RealmAccess>();

        realm.SetSetting(setting_key, Key);
    }

    public override string ToString()
    {
        return Language.GetLanguageName();
    }
}

