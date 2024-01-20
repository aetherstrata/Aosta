// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Platform;

namespace Aosta.Ava.Localization;

/// <summary>
/// Localization service
/// </summary>
internal sealed class Localizer : INotifyPropertyChanged, ILocalizer
{
    public static Localizer Instance { get; } = new();

    private const string indexer_name = "Item";
    private const string indexer_array_name = "Item[]";

    // The english localization is always kept in memory as a graceful fallback
    private readonly IReadOnlyDictionary<string, string> _fallback = getLocalization(InterfaceLanguage.English);

    private IReadOnlyDictionary<string, string> _localized = null!;
    private InterfaceLanguage _language = (InterfaceLanguage)(-1);

    public event PropertyChangedEventHandler? PropertyChanged;

    /// Current interface language of the application
    public InterfaceLanguage Language
    {
        get => _language;
        set
        {
            // Do not invalidate if language hasn't actually changed
            if (Language == value) return;

            // Reference the fallback dictionary instead of reading the file again if English is requested
            _localized = value == InterfaceLanguage.English
                ? _fallback
                : getLocalization(value);

            // Set the language and invalidate UI
            _language = value;
            invalidate();
        }
    }

    /// <summary>
    /// Access the localized string for the given key
    /// </summary>
    /// <param name="key">Key of the localized string</param>
    public string this[string key]
    {
        get
        {
            if (_localized.TryGetValue(key, out string? res))
                return res.Replace("\\n", "\n");

            // Try to get the english value before giving up
            return _fallback.TryGetValue(key, out string? fallbackRes)
                ? fallbackRes.Replace("\\n", "\n")
                : $"Could not find string {Language}:{key}";
        }
    }

    private void invalidate()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(indexer_name));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(indexer_array_name));
    }

    private static IReadOnlyDictionary<string, string> getLocalization(InterfaceLanguage language)
    {
        var uri = new Uri($"avares://{nameof(Aosta)}.{nameof(Ava)}/Assets/Localization/{language.GetLanguageCode()}.json");

        if (!AssetLoader.Exists(uri))
        {
            throw new IOException($"Could not find file {uri}");
        }

        using var sr = new StreamReader(AssetLoader.Open(uri), Encoding.UTF8);

        return JsonSerializer.Deserialize(sr.ReadToEnd(), LocalizationJsonContext.Default.IReadOnlyDictionaryStringString)
               ?? throw new JsonException($"Could not deserialize localization at {uri}");
    }
}

[JsonSerializable(typeof(IReadOnlyDictionary<string, string>))]
internal partial class LocalizationJsonContext : JsonSerializerContext;
