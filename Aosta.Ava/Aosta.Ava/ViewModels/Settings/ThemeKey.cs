// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Avalonia.Styling;

namespace Aosta.Ava.ViewModels.Settings;

internal sealed record ThemeKey
{
    public string Key { get; }

    public ThemeVariant Theme { get; }

    private ThemeKey(string key, ThemeVariant theme)
    {
        Key = key;
        Theme = theme;
    }

    private const string default_key = "Theme.System";
    private const string dark_key = "Theme.Dark";
    private const string light_key = "Theme.Light";

    public static readonly ThemeKey DEFAULT = new(default_key, ThemeVariant.Default);
    public static readonly ThemeKey DARK = new(dark_key, ThemeVariant.Dark);
    public static readonly ThemeKey LIGHT = new(light_key, ThemeVariant.Light);

    public static implicit operator string(ThemeKey key) => key.Key;

    public static explicit operator ThemeKey(string key) => key switch
    {
        dark_key => DARK,
        default_key => DEFAULT,
        light_key => LIGHT,
        _ => throw new InvalidCastException("String was not set to a valid theme key.")
    };
}
