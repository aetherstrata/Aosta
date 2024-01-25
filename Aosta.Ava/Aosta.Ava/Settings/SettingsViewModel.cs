// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Localization;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Aosta.Ava.Settings;

public class SettingsViewModel : ReactiveObject, IRoutableViewModel
{
    public SettingsViewModel(IScreen host)
    {
        HostScreen = host;
    }

    /// <inheritdoc />
    public string? UrlPathSegment => "settings";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    internal ThemeViewModel ThemeManager { get; } = new();

    internal InterfaceLanguageViewModel LanguageManager { get; } = new();

    [Reactive]
    public LocalizedString AppVersion { get; set; } = new("App.Version", App.Version);
}
