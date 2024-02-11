// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Aosta.Ava.Localization;

public sealed class LocalizedData<T> : ReactiveObject, ILocalized, IEquatable<LocalizedData<T>>
{
    /// <summary>
    /// The object associated with this localization.
    /// </summary>
    [NotNull]
    public T Data { get; }

    /// <summary>
    /// The localization key used to update this object's translation at runtime.
    /// </summary>
    public string Key { get; }

    /// <inheritdoc />
    [Reactive]
    public string Localized { get; private set; }

    public LocalizedData(T data, string key)
    {
        ArgumentNullException.ThrowIfNull(data);

        Data = data;
        Key = key;
        Localized = Localizer.Instance[Key];

        Localizer.Instance.PropertyChanged += (_, _) => Localized = Localizer.Instance[Key];
    }

    public LocalizedData(ILocalizable<T> localizable) : this(localizable.Data, localizable.Key)
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
}
