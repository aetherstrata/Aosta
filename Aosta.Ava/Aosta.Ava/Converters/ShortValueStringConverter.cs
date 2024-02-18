// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Globalization;

using Aosta.Ava.Localization;

using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;

namespace Aosta.Ava.Converters;

public class ShortValueStringConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            not null => value.ToString(),
            null => LocalizedString.NA.Localized,
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new InvalidOperationException($"The {nameof(ShortValueStringConverter)} converter shall only be used to convert objects to their string representation ");
    }
}
