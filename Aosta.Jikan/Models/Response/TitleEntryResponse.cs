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
    public required string Type { get; init; }

    /// <summary>
    /// Value of the Title.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }
}
