// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Aosta.Ava.Controls;

// ReSharper disable InconsistentNaming //Avalonia convention
public class SettingsEntry : TemplatedControl
{
    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<SettingsEntry, string>(nameof(Description),"Setting entry");

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly StyledProperty<Control> FooterProperty =
        AvaloniaProperty.Register<SettingsEntry, Control>(nameof(Footer), new ToggleSwitch());

    public Control Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }
}
