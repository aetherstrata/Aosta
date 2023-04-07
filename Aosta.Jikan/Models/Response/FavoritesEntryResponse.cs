﻿using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model with extended data for favorites section in user profile.
/// </summary>
public class FavoritesEntryResponse : MalImageSubItemResponse
{
	/// <summary>
	/// Type of resource
	/// </summary>
	[JsonPropertyName("type")]
	public string? Type { get; set; }

	/// <summary>
	/// Year when manga was published or anime started airing
	/// </summary>
	[JsonPropertyName("start_year")]
	public int? StartYear { get; set; }
}