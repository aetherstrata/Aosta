// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics.CodeAnalysis;

namespace Aosta.Ava.Localization;

public record LocalizableData<T>()
{
    public required T Data { get; init; }

    public required string LocalizedName { get; init; }

    [SetsRequiredMembers]
    public LocalizableData(T data, string localized) : this()
    {
        Data = data;
        LocalizedName = localized;
    }

    public override string ToString() => LocalizedName;
}
