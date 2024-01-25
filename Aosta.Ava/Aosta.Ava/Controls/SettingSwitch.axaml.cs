// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Aosta.Ava.Controls;

// ReSharper disable InconsistentNaming //Avalonia convention
public class SettingsEntry : TemplatedControl
{
    public static readonly StyledProperty<string> EntryProperty =
        AvaloniaProperty.Register<SettingsEntry, string>(nameof(Entry),"Setting entry");

    public string Entry
    {
        get => GetValue(EntryProperty);
        set => SetValue(EntryProperty, value);
    }

    public static readonly StyledProperty<Control> ActionProperty =
        AvaloniaProperty.Register<SettingsEntry, Control>(nameof(Action), new ToggleSwitch());

    public Control Action
    {
        get => GetValue(ActionProperty);
        set => SetValue(ActionProperty, value);
    }
}
