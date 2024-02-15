// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

namespace Aosta.Ava.Localization;

public abstract class LocalizedString(string key) : ReactiveObject, ILocalized, IDisposable, IEnableLogger
{
    public static readonly LocalizedString ALL           = Create("Label.All");
    public static readonly LocalizedString NOT_AVAILABLE = Create("Label.NotAvailable.Long");
    public static readonly LocalizedString NA            = Create("Label.NotAvailable.Short");

    /// <inheritdoc />
    public string LocalizationKey { get; } = key;

    /// <inheritdoc />
    public abstract string Localized { get; protected set; }

    public static LocalizedString CompactDate(DateTimeOffset dt)
    {
        return Create(compact_date_keys[dt.Month], dt.Day, dt.Year);
    }

    public static LocalizedString Duration(TimeSpan span)
    {
        return span switch
        {
            { Hours: > 0 }   => Create("Duration.Compact.Hours", span.Hours, span.Minutes, span.Seconds),
            { Minutes: > 0 } => Create("Duration.Compact.Minutes", span.Minutes, span.Seconds),
            _                => Create("Duration.Compact.Seconds", span.Seconds)
        };
    }

    public static LocalizedString Create(string key) => new ZeroArgs(key);

    public static LocalizedString Create(string key, object arg0) => new OneArg(key, arg0);

    public static LocalizedString Create(string key, object arg0, object arg1) => new TwoArgs(key, arg0, arg1);

    public static LocalizedString Create(string key, object arg0, object arg1, object arg2) => new ThreeArgs(key, arg0, arg1, arg2);

    public static LocalizedString Create(string key, params object[] args) => new ParamArgs(key, args);

    public static implicit operator string(LocalizedString localized) => localized.Localized;

    protected abstract void Dispose(bool disposing);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void logChange()
    {
        this.Log().Debug("Localization for {Key} changed: {NewValue}", LocalizationKey, Localized);
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

    #region Implementations

    private sealed class ZeroArgs : LocalizedString
    {
        [Reactive]
        public override string Localized { get; protected set; }

        public ZeroArgs(string key) : base(key)
        {
            Localized = Localizer.Instance[key];

            Localizer.Instance.PropertyChanged += onLanguageChange;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Localizer.Instance.PropertyChanged -= onLanguageChange;
            }
        }

        private void onLanguageChange(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Item")
            {
                Localized = Localizer.Instance[LocalizationKey];
                logChange();
            }
        }
    }

    private sealed class OneArg : LocalizedString
    {
        private readonly object _arg;

        [Reactive]
        public override string Localized { get; protected set; }

        public OneArg(string key, object arg0) : base(key)
        {
            _arg = arg0;

            Localized = string.Format(Localizer.Instance[key], _arg);

            Localizer.Instance.PropertyChanged += onLanguageChange;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Localizer.Instance.PropertyChanged -= onLanguageChange;
            }
        }

        private void onLanguageChange(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Item")
            {
                Localized = string.Format(Localizer.Instance[LocalizationKey], _arg);
                logChange();
            }
        }
    }

    private sealed class TwoArgs : LocalizedString
    {
        private readonly object _arg0;
        private readonly object _arg1;

        [Reactive]
        public override string Localized { get; protected set; }

        public TwoArgs(string key, object arg0, object arg1) : base(key)
        {
            _arg0 = arg0;
            _arg1 = arg1;

            Localized = string.Format(Localizer.Instance[key], _arg0, _arg1);

            Localizer.Instance.PropertyChanged += onLanguageChange;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Localizer.Instance.PropertyChanged -= onLanguageChange;
            }
        }

        private void onLanguageChange(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Item")
            {
                Localized = string.Format(Localizer.Instance[LocalizationKey], _arg0, _arg1);
                logChange();
            }
        }
    }

    private sealed class ThreeArgs : LocalizedString
    {
        private readonly object _arg0;
        private readonly object _arg1;
        private readonly object _arg2;

        [Reactive]
        public override string Localized { get; protected set; }

        public ThreeArgs(string key, object arg0, object arg1, object arg2) : base(key)
        {
            _arg0 = arg0;
            _arg1 = arg1;
            _arg2 = arg2;

            Localized = string.Format(Localizer.Instance[key], _arg0, _arg1, _arg2);

            Localizer.Instance.PropertyChanged += onLanguageChange;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Localizer.Instance.PropertyChanged -= onLanguageChange;
            }
        }

        private void onLanguageChange(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Item")
            {
                Localized = string.Format(Localizer.Instance[LocalizationKey], _arg0, _arg1, _arg2);
                logChange();
            }
        }
    }

    private sealed class ParamArgs : LocalizedString
    {
        private readonly object[] _args;

        [Reactive]
        public override string Localized { get; protected set; }

        public ParamArgs(string key, params object[] args) : base(key)
        {
            _args = args;

            Localized = string.Format(Localizer.Instance[key], _args);

            Localizer.Instance.PropertyChanged += onLanguageChange;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Localizer.Instance.PropertyChanged -= onLanguageChange;
            }
        }

        private void onLanguageChange(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Item")
            {
                Localized = string.Format(Localizer.Instance[LocalizationKey], _args);
                logChange();
            }
        }
    }

    #endregion
}
