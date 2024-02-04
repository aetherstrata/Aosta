// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

namespace Aosta.Ava.Settings;

public class SettingsViewModel : ReactiveObject, IRoutableViewModel
{
    public SettingsViewModel(IScreen host)
    {
        HostScreen = host;

        _realm.GetSetting(Setting.INCLUDE_NSFW, false, out _includeNsfw)
              .GetSetting(Setting.INCLUDE_UNAPPROVED, false, out _includeUnapproved);
    }

    /// <inheritdoc />
    public string? UrlPathSegment => "settings";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly RealmAccess _realm = Locator.Current.GetSafely<RealmAccess>();

    internal ThemeViewModel ThemeManager { get; } = new();

    internal InterfaceLanguageViewModel LanguageManager { get; } = new();

    public LocalizedString AppVersion { get; set; } = new("App.Version", App.Version);

    private bool _includeNsfw;
    public bool IncludeNsfw
    {
        get => _includeNsfw;
        set
        {
            if (value == _includeNsfw) return;

            this.RaisePropertyChanging();
            _realm.SetSetting(Setting.INCLUDE_NSFW, value);
            _includeNsfw = value;
            this.RaisePropertyChanged();
        }
    }

    private bool _includeUnapproved;
    public bool IncludeUnapproved
    {
        get => _includeUnapproved;
        set
        {
            if (value == _includeNsfw) return;

            this.RaisePropertyChanging();
            _realm.SetSetting(Setting.INCLUDE_UNAPPROVED, value);
            _includeUnapproved = value;
            this.RaisePropertyChanged();
        }
    }
}
