// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Aosta.Ava.Localization;

public record LocalizableData<TData>() : INotifyPropertyChanged
{
    public required TData Data { get; init; }

    public required string LocalizedName { get; set; }

    [SetsRequiredMembers]
    public LocalizableData(TData data, string localized) : this()
    {
        Data = data;
        LocalizedName = localized;
    }

    public override string ToString() => LocalizedName;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
