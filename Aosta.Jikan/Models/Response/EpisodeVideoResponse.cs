using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model class for episode video.
/// </summary>
public class EpisodeVideoResponse
{
	/// <summary>
	/// Episode's MyAnimeList Id.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; init; }

	/// <summary>
	/// Title of the episode video.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>
	/// Numbered title of the episode video.
	/// </summary>
	[JsonPropertyName("episode")]
	public string? NumberedTitle { get; init; }

	/// <summary>
	/// Url to episode video.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Episode's images set.
	/// </summary>
	[JsonPropertyName("Images")]
	public ImagesSetResponse? Images { get; init; }
}