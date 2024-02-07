// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
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
            .SetRawSource(key)
            .Property(
                new ClrPropertyInfo("Item", static o => Localizer.Instance[(string)o], null, typeof(string)),
                PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
            .Build();

        var binding = new CompiledBindingExtension(path);

        return binding.ProvideValue(serviceProvider);
    }
}
