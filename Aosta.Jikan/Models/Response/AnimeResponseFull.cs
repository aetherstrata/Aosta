using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Anime with full data model class.
/// </summary>
public class AnimeResponseFull : AnimeResponse
{
    /// <summary>
    /// Anime related entries.
    /// </summary>
    [JsonPropertyName("relations")]
    public ICollection<RelatedEntryResponse>? Relations { get; init; }

    /// <summary>
    /// Anime music themes (openings and endings).
    /// </summary>
    [JsonPropertyName("theme")]
    public AnimeThemesResponse? MusicThemes { get; init; }

    /// <summary>
    /// Anime external links.
    /// </summary>
    [JsonPropertyName("external")]
    public ICollection<ExternalLinkResponse>? ExternalLinks { get; init; }

    /// <summary>
    /// Anime streaming links.
    /// </summary>
    [JsonPropertyName("streaming")]
    public ICollection<ExternalLinkResponse>? StreamingLinks { get; init; }
}