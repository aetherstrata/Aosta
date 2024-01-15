// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

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
}
