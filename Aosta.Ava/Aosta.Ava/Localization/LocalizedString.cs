// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Serilog;

using Splat;

namespace Aosta.Ava.Localization;

public sealed class LocalizedString : ReactiveObject, ILocalized
{
    /// <inheritdoc />
    [Reactive]
    public string Localized { get; private set; }

    public static readonly LocalizedString NOT_AVAILABLE = new("Label.NotAvailable.Long");
    public static readonly LocalizedString NA = new("Label.NotAvailable.Short");

    public static LocalizedString Error(object? sender)
    {
        Log.Error("Could not get localization for {Object}", sender?.ToString());
        return new LocalizedString("Label.LocalizationLookupError", sender?.ToString());
    }

    public LocalizedString(string key)
    {
        Localized = Localizer.Instance[key];

        Localizer.Instance.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == "Item")
            {
                Localized = Localizer.Instance[key];
                Log.Verbose("Updated localized string for {Key} => {NewValue}", key, Localized);
            }
        };
    }

    public LocalizedString(string key, object? arg0)
    {
        Localized = string.Format(Localizer.Instance[key], arg0);

        Localizer.Instance.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == "Item")
            {
                Localized = string.Format(Localizer.Instance[key], arg0);
                Log.Verbose("Updated localized string for {Key} => {NewValue}", key, Localized);
            }
        };
    }

    public LocalizedString(string key, object? arg0, object? arg1)
    {
        Localized = string.Format(Localizer.Instance[key], arg0, arg1);

        Localizer.Instance.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == "Item")
            {
                Localized = string.Format(Localizer.Instance[key], arg0, arg1);
                Log.Verbose("Updated localized string for {Key} => {NewValue}", key, Localized);
            }
        };
    }

    public LocalizedString(string key, object? arg0, object? arg1, object? arg2)
    {
        Localized = string.Format(Localizer.Instance[key], arg0, arg1, arg2);

        Localizer.Instance.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == "Item")
            {
                Localized = string.Format(Localizer.Instance[key], arg0, arg1, arg2);
                Log.Verbose("Updated localized string for {Key} => {NewValue}", key, Localized);
            }
        };
    }

    public LocalizedString(string key, params object?[] args)
    {
        Localized = string.Format(Localizer.Instance[key], args);

        Localizer.Instance.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == "Item")
            {
                Localized = string.Format(Localizer.Instance[key], args);
                Log.Verbose("Updated localized string for {Key} => {NewValue}", key, Localized);
            }
        };
    }

    public static implicit operator string(LocalizedString localized)
    {
        return localized.Localized;
    }
}
