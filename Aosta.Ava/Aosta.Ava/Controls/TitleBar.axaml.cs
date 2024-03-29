// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using System.Windows.Input;

using Aosta.Ava.ViewModels;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Media;

namespace Aosta.Ava.Controls;

// ReSharper disable InconsistentNaming //Avalonia convention
public class TitleBar : TemplatedControl
{
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<TitleBar, string>(nameof(Title), "Title Text");

    public static readonly StyledProperty<bool> IsBackEnabledProperty =
        AvaloniaProperty.Register<TitleBar, bool>(nameof(IsBackEnabled), true);

    public static readonly StyledProperty<ICommand> BackCommandProperty =
        AvaloniaProperty.Register<TitleBar, ICommand>(nameof(BackCommand),
            ((MainViewModel)Application.Current!.DataContext!).GoBack);

    public static readonly StyledProperty<Control> MenuProperty =
        AvaloniaProperty.Register<TitleBar, Control>(nameof(Menu));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public bool IsBackEnabled
    {
        get => GetValue(IsBackEnabledProperty);
        set => SetValue(IsBackEnabledProperty, value);
    }

    public ICommand BackCommand
    {
        get => GetValue(BackCommandProperty);
        set => SetValue(BackCommandProperty, value);
    }

    public Control Menu
    {
        get => GetValue(MenuProperty);
        set => SetValue(MenuProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        var textBlock = this.GetTemplateChildren()
            .OfType<TextBlock>()
            .First(static x => x.Name == "PART_TitleText");

        textBlock.Tapped += (_, _) =>
        {
            // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
            textBlock.TextWrapping ^= TextWrapping.WrapWithOverflow;
        };
    }
}
