using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Anime details on the user list.
/// </summary>
public partial class AnimeListEntryDetailsResponse
{
    /// <summary>
    /// ID associated with MyAnimeList.
    /// </summary>
    [JsonPropertyName("mal_id")]
    public long? MalId { get; set; }

    /// <summary>
    /// Anime's canonical link.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Anime's images in various formats.
    /// </summary>
    [JsonPropertyName("images")]
    public ImagesSetResponse? Images { get; set; }

    /// <summary>
    /// Title of the anime.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Anime type (e. g. "TV", "Movie").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Anime's episode count.
    /// </summary>
    [JsonPropertyName("episodes")]
    public int? Episodes { get; set; }

    /// <summary>
    /// Anime's airing status (e. g. "Currently Airing").
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Is anime currently airing.
    /// </summary>
    [JsonPropertyName("airing")]
    public bool Airing { get; set; }

    /// <summary>
    /// Associative keys "from" and "to" which are alternative version of AiredString in ISO8601 format.
    /// </summary>
    [JsonPropertyName("aired")]
    public TimePeriodResponse? Aired { get; set; }

    /// <summary>
    /// Anime's age rating.
    /// </summary>
    [JsonPropertyName("rating")]
    public string? Rating { get; set; }

    /// <summary>
    /// Season of the year the anime premiered.
    /// </summary>
    [JsonPropertyName("season")]
    public string? Season { get; set; }

    /// <summary>
    /// Year the anime premiered.
    /// </summary>
    [JsonPropertyName("year")]
    public int? Year { get; set; }

    /// <summary>
    /// Anime's licensors numerically indexed with array values.
    /// </summary>
    [JsonPropertyName("licensors")]
    public IList<MalUrlResponse> Licensors { get; }

    /// <summary>
    /// Anime's studio(s) numerically indexed with array values.
    /// </summary>
    [JsonPropertyName("studios")]
    public IList<MalUrlResponse> Studios { get; }

    /// <summary>
    /// Anime's genres numerically indexed with array values.
    /// </summary>
    [JsonPropertyName("genres")]
    public IList<MalUrlResponse> Genres { get; }

    /// <summary>
    /// Anime's demographics
    /// </summary>
    [JsonPropertyName("demographics")]
    public IList<MalUrlResponse> Demographics { get; }
}