// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Serilog;

namespace Aosta.Ava.Localization;

public sealed class LocalizedString : ReactiveObject, ILocalized
{
    /// <inheritdoc />
    [Reactive]
    public string Localized { get; private set; }

    public static readonly LocalizedString ALL = new("Label.All");
    public static readonly LocalizedString NOT_AVAILABLE = new("Label.NotAvailable.Long");
    public static readonly LocalizedString NA = new("Label.NotAvailable.Short");

    public static LocalizedString Error(object? sender)
    {
        Log.Error("Could not get localization for {Object}", sender?.ToString());
        return new LocalizedString("Label.LocalizationLookupError", sender?.ToString());
    }

    public static LocalizedString CompactDate(DateTimeOffset dt)
    {
        return new LocalizedString(compact_date_keys[dt.Month], dt.Day, dt.Year);
    }

    public LocalizedString(string key)
    {
        Localized = Localizer.Instance[key];

        Localizer.Instance.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == "Item")
            {
                Localized = Localizer.Instance[key];
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
            }
        };
    }

    public static implicit operator string(LocalizedString localized)
    {
        return localized.Localized;
    }

    private static readonly string[] month_name =
    [
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December",
    ];

    private static readonly FrozenDictionary<int, string> compact_date_keys = Enumerable
        .Range(1, 12)
        .Select(month => new KeyValuePair<int, string>(month, $"Date.Compact.{month_name[month-1]}"))
        .ToFrozenDictionary();
}
