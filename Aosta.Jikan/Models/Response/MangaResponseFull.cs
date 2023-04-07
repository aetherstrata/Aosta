using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Manga with full data model class.
/// </summary>
public class MangaResponseFull: MangaResponse
{
    /// <summary>
    /// Manga related entries.
    /// </summary>
    [JsonPropertyName("relations")]
    public ICollection<RelatedEntryResponse>? Relations { get; set; }

    /// <summary>
    /// Manga external links.
    /// </summary>
    [JsonPropertyName("external")]
    public ICollection<ExternalLinkResponse>? ExternalLinks { get; set; }
}