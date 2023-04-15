using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Class representing details about entry for recommendation
/// </summary>
public class RecommendationEntryResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; init; }

	/// <summary>
	/// Recommendation entry title's name.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>
	/// Url to Recommendation entry main page.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Recommendation entry's images set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }
}