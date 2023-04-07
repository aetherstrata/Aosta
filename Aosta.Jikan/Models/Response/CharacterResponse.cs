﻿using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Character model class.
/// </summary>
public class CharacterResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; set; }

	/// <summary>
	/// Character's page url.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Character's name.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }
		
	/// <summary>
	/// Character's name in kanji.
	/// </summary>
	[JsonPropertyName("name_kanji")]
	public string? NameKanji { get; set; }

	/// <summary>
	/// Character's nicknames.
	/// </summary>
	[JsonPropertyName("nicknames")]
	public ICollection<string>? Nicknames { get; set; }

	/// <summary>
	/// About character
	/// </summary>
	[JsonPropertyName("about")]
	public string? About { get; set; }

	/// <summary>
	/// Character favourite count on MyAnimeList.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int? Favorites { get; set; }

	/// <summary>
	/// Character's image set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; set; }
}