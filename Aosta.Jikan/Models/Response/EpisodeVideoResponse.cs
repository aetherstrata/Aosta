﻿using System.Text.Json.Serialization;

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
	public long MalId { get; set; }

	/// <summary>
	/// Title of the episode video.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// Numbered title of the episode video.
	/// </summary>
	[JsonPropertyName("episode")]
	public string? NumberedTitle { get; set; }

	/// <summary>
	/// Url to episode video.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Episode's images set.
	/// </summary>
	[JsonPropertyName("Images")]
	public ImagesSetResponse? Images { get; set; }
}