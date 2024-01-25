// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Aosta.Ava.Localization;

public sealed class LocalizedString : ReactiveObject, ILocalized
{
    /// <inheritdoc />
    [Reactive]
    public string Localized { get; private set; }

    public LocalizedString(string key)
    {
        Localized = Localizer.Instance[key];

        Localizer.Instance.PropertyChanged += (_, _) => Localized = Localizer.Instance[key];
    }

    public LocalizedString(string key, params object[] args)
    {
        Localized = string.Format(Localizer.Instance[key], args);

        Localizer.Instance.PropertyChanged += (_, _) => Localized = string.Format(Localizer.Instance[key], args);
    }

    public override string ToString() => Localized;
}
