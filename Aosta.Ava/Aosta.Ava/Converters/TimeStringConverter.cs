// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Globalization;

using Aosta.Ava.Localization;

using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;

namespace Aosta.Ava.Converters;

public sealed class TimeStringConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            double time => parse(TimeSpan.FromSeconds(time)),
            TimeSpan span => parse(span),
            _ => null
        };

        static string parse(TimeSpan span) => span switch
        {
            { Hours: > 0 }   => string.Format(Localizer.Instance["Duration.Compact.Hours"], span.Hours, span.Minutes, span.Seconds),
            { Minutes: > 0 } => string.Format(Localizer.Instance["Duration.Compact.Minutes"], span.Minutes, span.Seconds),
            _                => string.Format(Localizer.Instance["Duration.Compact.Seconds"], span.Seconds)
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new InvalidOperationException($"The {nameof(TimeStringConverter)} converter shall be used only to display time values as text in UI elements");
    }
}
