﻿using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Model class for promo video.
/// </summary>
public class PromoVideoResponse
{
	/// <summary>
	/// Title of the promotional video.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// Trailer to promo video
	/// </summary>
	[JsonPropertyName("trailer")]
	public AnimeTrailerResponse? Trailer { get; set; }
}