// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data;
using Aosta.Data.Extensions;

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

    /// <summary>
    /// The Avalonia theme variant of this application theme.
    /// </summary>
    public ThemeVariant Theme { get; }

    private ThemeKey(string key, ThemeVariant theme)
    {
        Key = key;
        Theme = theme;
    }

    ThemeKey ILocalizable<ThemeKey>.Data => this;

    public LocalizedData<ThemeKey> Localize() => new(this);

    /// <summary>
    /// Read the current theme from Realm.
    /// </summary>
    /// <returns>The current theme stored on Realm.</returns>
    public static ThemeKey Load()
    {
        var realm = Locator.Current.GetSafely<RealmAccess>();

        string setting = realm.GetSetting<string>(setting_key, default_key);

        return setting switch
        {
            dark_key => DARK,
            light_key => LIGHT,
            _ => DEFAULT
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
