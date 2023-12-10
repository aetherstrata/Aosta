using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Title model class
/// </summary>
public class TitleEntryResponse
{
    /// <summary>
    /// Type of title (usually the language).
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Value of the Title.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }
}
