// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Aosta.Ava.Localization;

public interface ILocalized
{
    /// The key used to retrieve the localization.
    public string LocalizationKey { get; }

    /// The localized string.
    public string Localized { get; }
}
