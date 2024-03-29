﻿using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model for animeography entry
/// </summary>
public class CharacterAnimeographyEntryResponse
{
	/// <summary>
	/// Character's animeography entry.
	/// </summary>
	[JsonPropertyName("anime")]
	public MalImageSubItemResponse? Anime { get; init; }

	/// <summary>
	/// Role of character in sub item (anime or manga). Not available in all requests.
	/// </summary>
	[JsonPropertyName("role")]
	public string? Role { get; init; }
}