// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;

using Aosta.Ava.Localization;
using Aosta.Common.Extensions;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.Settings;

internal class InterfaceLanguageViewModel : ReactiveObject
{
    internal InterfaceLanguageViewModel()
    {
        _currentLanguage = LanguageKey.Load().OrDefault(LanguageKey.DEFAULT);

        Languages = LanguageKey.All();
    }

    internal IList<LanguageKey> Languages { get; }

    private LanguageKey _currentLanguage;
    internal LanguageKey CurrentLanguage
    {
        get => _currentLanguage;
        set
        {
            value.Save();
            Localizer.Instance.Language = value.Language;
            this.Log().Info("Application language set to {Language}", value.Name);
            this.RaiseAndSetIfChanged(ref _currentLanguage, value);
        }
    }
}
