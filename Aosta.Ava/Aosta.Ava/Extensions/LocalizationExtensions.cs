// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Localization;

namespace Aosta.Ava.Extensions;

public static class LocalizationExtensions
{
    public static LocalizedData<T> WrapAndLocalize<T>(this T obj, string key)
    {
        return new LocalizedData<T>(obj, key);
    }

    public static LocalizedString Localize(this string key)
    {
        return LocalizedString.Create(key);
    }

    public static LocalizedString Localize(this (string key, object arg0) tuple)
    {
        return LocalizedString.Create(tuple.key, tuple.arg0);
    }

    public static LocalizedString Localize(this (string key, object arg0, object arg1) tuple)
    {
        return LocalizedString.Create(tuple.key, tuple.arg0, tuple.arg1);
    }

    public static LocalizedString Localize(this (string key, object arg0, object arg1, object arg2) tuple)
    {
        return LocalizedString.Create(tuple.key, tuple.arg0, tuple.arg1, tuple.arg2);
    }
}
