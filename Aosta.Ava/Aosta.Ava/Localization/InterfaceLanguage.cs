// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;

namespace Aosta.Ava.Localization;

public enum InterfaceLanguage
{
    English,
    Italian
}

public static class InterfaceLanguageExtensions
{
    internal static string GetLanguageName(this InterfaceLanguage uiLangEnum) => uiLangEnum switch
    {
        InterfaceLanguage.English => "English",
        InterfaceLanguage.Italian => "Italiano",
        _ => throw new UnreachableException()
    };

    internal static string GetLanguageCode(this InterfaceLanguage uiLangEnum) => uiLangEnum switch
    {
        InterfaceLanguage.English => "en-US",
        InterfaceLanguage.Italian => "it-IT",
        _ => throw new UnreachableException()
    };
}
