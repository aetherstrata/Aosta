// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        _currentTheme = new LocalizableData<ThemeVariant>(stored.Theme, stored.Key);
    }

    [Reactive]
    public ObservableCollection<LocalizableData<ThemeVariant>> AppThemes { get; set; } =
        new(getLocalizedThemes().Select(x =>
            new LocalizableData<ThemeVariant>(x.Key, x.Value)));

    private LocalizableData<ThemeVariant> _currentTheme;
    public LocalizableData<ThemeVariant> CurrentAppTheme
    {
        get => _currentTheme;
        set
        {
            if (_currentTheme.Data.Equals(value.Data)) return;

            this.RaisePropertyChanging();

            _currentTheme = value;
            _aosta.Realm.SetSetting(Settings.AppTheme, getThemeKey(value.Data));

            Application.Current!.RequestedThemeVariant = value.Data;
            _aosta.Log.Information("Application theme set to {Variant}", getThemeKey(value.Data));

            this.RaisePropertyChanged();
        }
    }

    private void updateThemeList()
    {
        var previous = CurrentAppTheme.Data ?? ThemeVariant.Default;
        var updatedLocalizations = getLocalizedThemes();

        AppThemes = new ObservableCollection<LocalizableData<ThemeVariant>>(updatedLocalizations
            .Select(pair => new LocalizableData<ThemeVariant>(pair.Key, pair.Value)));

        CurrentAppTheme = new LocalizableData<ThemeVariant>(previous, updatedLocalizations[previous]);
    }

    private static Dictionary<ThemeVariant, string> getLocalizedThemes() => new()
    {
        { ThemeVariant.Default, Localizer.Instance[ThemeKey.DEFAULT] },
        { ThemeVariant.Dark, Localizer.Instance[ThemeKey.DARK] },
        { ThemeVariant.Light, Localizer.Instance[ThemeKey.LIGHT] }
    };

    private static string getThemeKey(ThemeVariant theme) => theme.Key switch
    {
        nameof(ThemeVariant.Dark) => ThemeKey.DARK,
        nameof(ThemeVariant.Light) => ThemeKey.LIGHT,
        _ => ThemeKey.DEFAULT
    };

}
