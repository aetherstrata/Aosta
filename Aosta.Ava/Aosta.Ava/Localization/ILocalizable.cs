// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using ReactiveUI;

namespace Aosta.Ava.Localization;

public interface ILocalizable<T>
{
    string Key { get; }

    LocalizedData<T> Localize();
}
