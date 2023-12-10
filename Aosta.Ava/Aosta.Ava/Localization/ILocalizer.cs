// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Aosta.Ava.Localization;

/// <summary>
/// Interface for the localization service
/// </summary>
internal interface ILocalizer
{
    /// <summary>
    /// Get the current language code
    /// </summary>
    /// <example>"en-US"</example>
    /// <value> Current language code </value>
    InterfaceLanguage Language { get; }

    /// <summary>
    /// Get the localized string for the given key
    /// </summary>
    /// <param name="key"> String key </param>
    /// <returns> Localized sentence </returns>
    string this[string key]
    {
        get;
    }
}
