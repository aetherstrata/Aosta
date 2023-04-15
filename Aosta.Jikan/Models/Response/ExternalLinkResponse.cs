using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model for external links for manga/anime
/// </summary>
public class ExternalLinkResponse
{
    /// <summary>
    /// Name of the external service.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    
    /// <summary>
    /// Url to external service.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}