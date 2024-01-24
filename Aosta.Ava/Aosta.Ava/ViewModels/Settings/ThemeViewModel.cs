// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.ObjectModel;
using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Common.Extensions;
using Aosta.Core;

using Avalonia;
using Avalonia.Styling;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

namespace Aosta.Ava.ViewModels.Settings;

public class ThemeViewModel : ReactiveObject
{
    private readonly AostaDotNet _aosta = Locator.Current.GetSafely<AostaDotNet>();

    internal ThemeViewModel()
    {
        Localizer.Instance.PropertyChanged += (_, _) => updateThemeList();

        var stored = (ThemeKey)_aosta.Realm.GetSetting<string>(Settings.AppTheme).OrDefault(ThemeKey.DEFAULT.Key);

        _currentTheme = stored.Localize();
    }

    [Reactive]
    public ObservableCollection<LocalizableData<ThemeVariant>> AppThemes { get; set; } =
    [
        ThemeKey.DEFAULT.Localize(),
        ThemeKey.DARK.Localize(),
        ThemeKey.LIGHT.Localize()
    ];

    private LocalizableData<ThemeVariant> _currentTheme;
    public LocalizableData<ThemeVariant> CurrentAppTheme
    {
        get => _currentTheme;
        set
        {
            if (_currentTheme.Data.Equals(value.Data)) return;

            this.RaisePropertyChanging();

            _currentTheme = value;
            _aosta.Realm.SetSetting(Settings.AppTheme, value.Data.GetThemeKey().Key);

            Application.Current!.RequestedThemeVariant = value.Data;
            _aosta.Log.Information("Application theme set to {Variant}", value.Data.GetThemeKey().Key);

            this.RaisePropertyChanged();
        }
    }

    private void updateThemeList()
    {
        foreach (var entry in AppThemes)
        {
            entry.LocalizedName = entry.Data.GetLocalizedKey();
        }
    }
}

static file class ThemeVariantExtensions
{
    public static ThemeKey GetThemeKey(this ThemeVariant theme) => theme.Key switch
    {
        nameof(ThemeVariant.Dark) => ThemeKey.DARK,
        nameof(ThemeVariant.Light) => ThemeKey.LIGHT,
        _ => ThemeKey.DEFAULT
    };

    public static string GetLocalizedKey(this ThemeVariant theme) => Localizer.Instance[theme.GetThemeKey()];
}
