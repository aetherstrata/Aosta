// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Localization;
using Aosta.Ava.ViewModels.Settings;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class SettingsViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment => "settings";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    public SettingsViewModel(IScreen host)
    {
        HostScreen = host;
    }

    public ThemeViewModel ThemeManager { get; } = new();

    public string AppVersion => string.Format(Localizer.Instance["App.Version"], App.Version);
}
