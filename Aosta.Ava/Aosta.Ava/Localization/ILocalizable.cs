// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Aosta.Ava.Localization;

public interface ILocalizable<T>
{
    string LocalizationKey { get; }

    T Data { get; }

    LocalizedData<T> Localize();
}

public interface ILocalizable
{
    string LocalizationKey { get; }

    LocalizedString Localize();
}
