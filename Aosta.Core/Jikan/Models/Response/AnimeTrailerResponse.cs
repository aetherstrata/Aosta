using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Anime trailer model class.
/// </summary>
public class AnimeTrailerResponse
{
	/// <summary>
	/// ID associated with Youtube.
	/// </summary>
	[JsonPropertyName("youtube_id")]
	public string? YoutubeId { get; set; }

	/// <summary>
	/// Url to the video.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Embed url to the video.
	/// </summary>
	[JsonPropertyName("embed_url")]
	public string? EmbedUrl { get; set; }

	/// <summary>
	/// Image related to the trailer in various resolutions.
	/// </summary>
	[JsonPropertyName("images")]
	public ImageResponse? Image { get; set; }
}