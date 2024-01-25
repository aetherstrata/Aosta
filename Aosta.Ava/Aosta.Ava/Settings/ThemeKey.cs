// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Core.Database;

using Avalonia.Styling;

using Splat;

namespace Aosta.Ava.Settings;

public sealed record ThemeKey : ISetting<ThemeKey>, ILocalizable<ThemeKey>
{
    private const string setting_key = "AppTheme";

    /// <summary>
    /// The localization key for this theme name.
    /// </summary>
    public string Key { get; }

    string ILocalizable<ThemeKey>.Key => Key;

    /// <summary>
    /// The Avalonia theme variant of this application theme.
    /// </summary>
    public ThemeVariant Theme { get; }

    private ThemeKey(string key, ThemeVariant theme)
    {
        Key = key;
        Theme = theme;
    }

    public LocalizedData<ThemeKey> Localize() => new(this, Key);

    /// <summary>
    /// Read the current value from Realm.
    /// </summary>
    /// <returns>The current theme stored on Realm.</returns>
    public static ThemeKey? Load()
    {
        var realm = Locator.Current.GetSafely<RealmAccess>();

        string? setting = realm.GetSetting<string>(setting_key);

        return setting switch
        {
            dark_key => DARK,
            default_key => DEFAULT,
            light_key => LIGHT,
            _ => null
        };
    }

    /// <summary>
    /// Save this theme to Realm.
    /// </summary>
    public void Save()
    {
        var realm = Locator.Current.GetSafely<RealmAccess>();

        realm.SetSetting(setting_key, Key);
    }

    private const string default_key = "Theme.System";
    private const string dark_key = "Theme.Dark";
    private const string light_key = "Theme.Light";

    public static readonly ThemeKey DEFAULT = new(default_key, ThemeVariant.Default);
    public static readonly ThemeKey DARK = new(dark_key, ThemeVariant.Dark);
    public static readonly ThemeKey LIGHT = new(light_key, ThemeVariant.Light);

    public static implicit operator string(ThemeKey key) => key.Key;
}
