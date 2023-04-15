using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Anime details on the user list.
/// </summary>
public class MangaListEntryDetailsResponse
{
    /// <summary>
    /// ID associated with MyAnimeList.
    /// </summary>
    [JsonPropertyName("mal_id")]
    public long MalId { get; init; }

    /// <summary>
    /// Title of the manga.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Manga's URL
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Manga's image set
    /// </summary>
    [JsonPropertyName("images")]
    public ImagesSetResponse? Images { get; init; }

    /// <summary>
    /// Manga type (e. g. "Manga", "Light novel").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Manga's chapters total count. 0 if not finished.
    /// </summary>
    [JsonPropertyName("chapters")]
    public int? Chapters { get; init; }

    /// <summary>
    /// Manga's volumes total count. 0 if not finished.
    /// </summary>
    [JsonPropertyName("volumes")]
    public int? Volumes { get; init; }

    /// <summary>
    /// Anime's airing status (e. g. "Publishing").
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>
    /// Is manga currently being published.
    /// </summary>
    [JsonPropertyName("publishing")]
    public bool Publishing { get; init; }

    /// <summary>
    /// Associative keys "from" and "to" .
    /// </summary>
    [JsonPropertyName("published")]
    public TimePeriodResponse? Published { get; init; }

    /// <summary>
    /// Manga's magazines numerically indexed with array values.
    /// </summary>
    [JsonPropertyName("magazines")]
    public ICollection<MalUrlResponse>? Magazines { get; init; }

    /// <summary>
    /// Manga's genres numerically indexed with array values.
    /// </summary>
    [JsonPropertyName("genres")]
    public ICollection<MalUrlResponse>? Genres { get; init; }

    /// <summary>
    /// Manga's demographics
    /// </summary>
    [JsonPropertyName("demographics")]
    public ICollection<MalUrlResponse>? Demographics { get; init; }
}