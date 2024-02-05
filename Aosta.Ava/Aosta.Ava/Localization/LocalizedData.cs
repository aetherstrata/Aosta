// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Aosta.Ava.Localization;

public sealed class LocalizedData<T>() : ReactiveObject, ILocalized, IEquatable<LocalizedData<T>>
{
    /// <summary>
    /// The object associated with this localization.
    /// </summary>
    [NotNull]
    public required T Data { get; init; }

    /// <summary>
    /// The localization key used to update this object's translation at runtime.
    /// </summary>
    public required string Key { get; init; }

    /// <inheritdoc />
    [Reactive]
    public string Localized { get; private set; }

    [SetsRequiredMembers]
    public LocalizedData(T data, string key) : this()
    {
        Data = data;
        Key = key;
        Localized = Localizer.Instance[Key];

        Localizer.Instance.PropertyChanged += (_, _) => Localized = Localizer.Instance[Key];
    }

    public override string ToString() => Localized;

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
