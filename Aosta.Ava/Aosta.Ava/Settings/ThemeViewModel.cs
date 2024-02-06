// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Collections.ObjectModel;

using Aosta.Ava.Localization;
using Aosta.Common.Extensions;

using Avalonia;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

namespace Aosta.Ava.Settings;

internal class ThemeViewModel : ReactiveObject
{
    internal ThemeViewModel()
    {
        var stored = ThemeKey.Load().OrDefault(ThemeKey.DEFAULT);

        _currentTheme = stored.Localize();
    }

    public IList<LocalizedData<ThemeKey>> AppThemes { get; set; } =
    [
        ThemeKey.DEFAULT.Localize(),
        ThemeKey.DARK.Localize(),
        ThemeKey.LIGHT.Localize()
    ];

    private LocalizedData<ThemeKey> _currentTheme;

    public LocalizedData<ThemeKey> CurrentTheme
    {
        get => _currentTheme;
        set
        {
            if (!_currentTheme.Data.Equals(value.Data))
            {
                value.Data.Save();
                Application.Current!.RequestedThemeVariant = value.Data.Theme;
                this.Log().Info("Application theme set to {Variant}", value.Data.Key);
            }

            this.RaiseAndSetIfChanged(ref _currentTheme, value);
        }
    }
}
