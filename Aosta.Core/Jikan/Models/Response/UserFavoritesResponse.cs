﻿using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Model representing user favorites
/// </summary>
public class UserFavoritesResponse
{
	/// <summary>
	/// User's favorite anime.
	/// </summary>
	[JsonPropertyName("anime")]
	public ICollection<FavoritesEntryResponse>? Anime { get; set; }

	/// <summary>
	/// User's favorite manga.
	/// </summary>
	[JsonPropertyName("manga")]
	public ICollection<FavoritesEntryResponse>? Manga { get; set; }

	/// <summary>
	/// User's favorite characters.
	/// </summary>
	[JsonPropertyName("characters")]
	public ICollection<MalImageSubItemResponse>? Characters { get; set; }

	/// <summary>
	/// User's favorite people.
	/// </summary>
	[JsonPropertyName("people")]
	public ICollection<MalImageSubItemResponse>? People { get; set; }
}