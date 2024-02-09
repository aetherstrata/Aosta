// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Avalonia.Data;
using Avalonia.Data.Core;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;

namespace Aosta.Ava.Localization;

internal class LocalizeExtension(string key) : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var path = new CompiledBindingPathBuilder()
            .SetRawSource(Localizer.Instance)
            .Property(
                new ClrPropertyInfo("Item", _ => Localizer.Instance[key], null, typeof(string)),
                PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
            .Build();

        var binding = new CompiledBindingExtension
        {
            Mode = BindingMode.OneWay,
            Path = path,
        };

        return binding.ProvideValue(serviceProvider);
    }
}
