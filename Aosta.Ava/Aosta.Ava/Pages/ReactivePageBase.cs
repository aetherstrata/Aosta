// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Avalonia.Interactivity;
using Avalonia.ReactiveUI;

using Splat;

namespace Aosta.Ava.Pages;

public abstract class ReactivePageBase<T> : ReactiveUserControl<T>, IEnableLogger where T : class
{
    protected override void OnUnloaded(RoutedEventArgs e)
    {
        if (ViewModel is IDisposable disposable)
        {
            this.Log().Debug("Dispose invoked on viewmodel of type {Type}", typeof(T));
            disposable.Dispose();
        }

        base.OnUnloaded(e);
    }
}
