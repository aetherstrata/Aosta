using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Anime episode model class.
/// </summary>
public class AnimeEpisodeResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public required long MalId { get; init; }

	/// <summary>
	/// URL to the episode.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Title of the episode.
	/// </summary>
	[JsonPropertyName("title")]
	public required string Title { get; init; }

	/// <summary>
	/// Title of the anime in Japanese.
	/// </summary>
	[JsonPropertyName("title_japanese")]
	public string? TitleJapanese { get; init; }

	/// <summary>
	/// Title of the anime in romaji.
	/// </summary>
	[JsonPropertyName("title_romanji")]
	public string? TitleRomanji { get; init; }

	/// <summary>
	/// Episode's duration.
	/// </summary>
	[JsonPropertyName("duration")]
	public int? Duration { get; init; }

	/// <summary>
	/// Date when episode aired at first.
	/// </summary>
	[JsonPropertyName("aired")]
	public required DateTimeOffset Aired { get; init; }

	/// <summary>
	/// Is the episode filler.
	/// </summary>
	[JsonPropertyName("filler")]
	public required bool Filler { get; init; }

	/// <summary>
	/// Is the episode recap.
	/// </summary>
	[JsonPropertyName("recap")]
	public required bool Recap { get; init; }

    /// <summary>
    /// The score of the episode
    /// </summary>
    /// <remarks>It's value can be between 0 and 5</remarks>
    [JsonPropertyName("score")]
    [Range(0d, 5d)]
    public double? Score { get; init; }

	/// <summary>
	/// Episode's synopsis.
	/// </summary>
	[JsonPropertyName("synopsis")]
	public string? Synopsis { get; init; }

	/// <summary>
	/// URL to forum discussion
	/// </summary>
	[JsonPropertyName("forum_url")]
	public string? ForumUrl{ get; init; }
}
