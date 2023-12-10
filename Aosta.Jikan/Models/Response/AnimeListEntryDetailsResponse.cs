using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Anime details on the user list.
/// </summary>
public class AnimeListEntryDetailsResponse
{
    /// <summary>
    /// ID associated with MyAnimeList.
    /// </summary>
    [JsonPropertyName("mal_id")]
    public long? MalId { get; init; }

    /// <summary>
    /// Anime's canonical link.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// Anime's images in various formats.
    /// </summary>
    [JsonPropertyName("images")]
    public ImagesSetResponse? Images { get; init; }

    /// <summary>
    /// Title of the anime.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Anime type (e. g. "TV", "Movie").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Anime's episode count.
    /// </summary>
    [JsonPropertyName("episodes")]
    public int? Episodes { get; init; }

    /// <summary>
    /// Anime's airing status (e. g. "Currently Airing").
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>
    /// Is anime currently airing.
    /// </summary>
    [JsonPropertyName("airing")]
    public bool Airing { get; init; }

    /// <summary>
    /// Associative keys "from" and "to" which are alternative version of AiredString in ISO8601 format.
    /// </summary>
    [JsonPropertyName("aired")]
    public TimePeriodResponse? Aired { get; init; }

    /// <summary>
    /// Anime's age rating.
    /// </summary>
    [JsonPropertyName("rating")]
    public string? Rating { get; init; }

    /// <summary>
    /// Season of the year the anime premiered.
    /// </summary>
    [JsonPropertyName("season")]
    public string? Season { get; init; }

    /// <summary>
    /// Year the anime premiered.
    /// </summary>
    [JsonPropertyName("year")]
    public int? Year { get; init; }

    /// <summary>
    /// Anime's licensors numerically indexed with array values.
    /// </summary>
    [JsonPropertyName("licensors")]
    public IList<MalUrlResponse> Licensors { get; } = [];

    /// <summary>
    /// Anime's studio(s) numerically indexed with array values.
    /// </summary>
    [JsonPropertyName("studios")]
    public IList<MalUrlResponse> Studios { get; } = [];

    /// <summary>
    /// Anime's genres numerically indexed with array values.
    /// </summary>
    [JsonPropertyName("genres")]
    public IList<MalUrlResponse> Genres { get; } = [];

    /// <summary>
    /// Anime's demographics
    /// </summary>
    [JsonPropertyName("demographics")]
    public IList<MalUrlResponse> Demographics { get; } = [];
}
