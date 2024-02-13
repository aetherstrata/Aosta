// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Globalization;

using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;

namespace Aosta.Ava.Converters;

public class NullValueBoolConverter : MarkupExtension, IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string val)
        {
            return !string.IsNullOrEmpty(val);
        }

        return value != null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
