// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Aosta.Ava.Localization;

public sealed class LocalizedData<T> : ReactiveObject, ILocalized, IDisposable, IEquatable<LocalizedData<T>>
{
    /// <summary>
    /// The object associated with this localization.
    /// </summary>
    [NotNull]
    public T Data { get; }

    /// <summary>
    /// The localization key used to update this object's translation at runtime.
    /// </summary>
    public string LocalizationKey { get; }

    /// <inheritdoc />
    [Reactive]
    public string Localized { get; private set; }

    public LocalizedData(T data, string key)
    {
        ArgumentNullException.ThrowIfNull(data);

        Data = data;
        LocalizationKey = key;
        Localized = Localizer.Instance[LocalizationKey];

        Localizer.Instance.PropertyChanged += onLanguageChange;
    }

    public LocalizedData(ILocalizable<T> localizable) : this(localizable.Data, localizable.LocalizationKey)
    {
    }

    public bool Equals(LocalizedData<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T>.Default.Equals(Data, other.Data);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is LocalizedData<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Data);
    }

    public void Dispose()
    {
        Localizer.Instance.PropertyChanged -= onLanguageChange;
    }

    private void onLanguageChange(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Item")
        {
            Localized = Localizer.Instance[LocalizationKey];
        }
    }
}
