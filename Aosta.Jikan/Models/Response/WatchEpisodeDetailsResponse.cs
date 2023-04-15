using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model of entry details in recent/popular episodes list.
/// </summary>
public class WatchEpisodeDetailsResponse
{
    /// <summary>
    /// ID associated with MyAnimeList.
    /// </summary>
    [JsonPropertyName("mal_id")]
    public long MalId { get; init; }

    /// <summary>
    /// Is episode premium.
    /// </summary>
    [JsonPropertyName("premium")]
    public bool? Premium { get; init; }

    /// <summary>
    /// Episode's title.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Url to sub item main page.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}