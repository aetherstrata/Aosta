// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Ava.Models;
using Aosta.Common.Extensions;
using Aosta.Core;

using Avalonia;

using DynamicData.Binding;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

namespace Aosta.Ava.ViewModels.Settings;

internal class ThemeViewModel : ReactiveObject
{
    private readonly AostaDotNet _aosta = Locator.Current.GetSafely<AostaDotNet>();

    internal ThemeViewModel()
    {
        var stored = ThemeKey.Load().OrDefault(ThemeKey.DEFAULT);

        _currentTheme = stored.Localize();
    }

    [Reactive]
    public ObservableCollection<LocalizedData<ThemeKey>> AppThemes { get; set; } =
    [
        ThemeKey.DEFAULT.Localize(),
        ThemeKey.DARK.Localize(),
        ThemeKey.LIGHT.Localize()
    ];

    private LocalizedData<ThemeKey> _currentTheme;

    public LocalizedData<ThemeKey> CurrentAppTheme
    {
        get => _currentTheme;
        set
        {
            if (!_currentTheme.Data.Equals(value.Data))
            {
                value.Data.Save();
                Application.Current!.RequestedThemeVariant = value.Data.Theme;
                _aosta.Log.Information("Application theme set to {Variant}", value.Data.Key);
            }

            this.RaiseAndSetIfChanged(ref _currentTheme, value);
        }
    }
}
