using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Anime related videos list model class.
/// </summary>
public class AnimeVideosResponse
{
	/// <summary>
	/// Anime's related promo videos URLs.
	/// </summary>
	[JsonPropertyName("promo")]
	public ICollection<PromoVideoResponse>? PromoVideos { get; init; }

	/// <summary>
	/// Anime's related episode videos URLs.
	/// </summary>
	[JsonPropertyName("episodes")]
	public ICollection<EpisodeVideoResponse>? EpisodeVideos { get; init; }
		
	/// <summary>
	/// Anime's related music videos URLs.
	/// </summary>
	[JsonPropertyName("music_videos")]
	public ICollection<MusicVideoResponse>? MusicVideos { get; init; }
}