// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;

using ReactiveUI;

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

    internal LanguageViewModel LanguageManager { get; } = new();

    public ILocalized AppVersion { get; set; } = ("App.Version", App.VERSION).Localize();

    private bool _includeNsfw = Setting.IncludeNsfw;
    public bool IncludeNsfw
    {
        get => _includeNsfw;
        set
        {
            if (value == _includeNsfw) return;

            this.RaisePropertyChanging();
            Setting.IncludeNsfw = value;
            _includeNsfw = value;
            this.RaisePropertyChanged();
        }
    }

    private bool _includeUnapproved = Setting.IncludeUnapproved;
    public bool IncludeUnapproved
    {
        get => _includeUnapproved;
        set
        {
            if (value == _includeUnapproved) return;

            this.RaisePropertyChanging();
            Setting.IncludeUnapproved = value;
            _includeUnapproved = value;
            this.RaisePropertyChanged();
        }
    }
}
