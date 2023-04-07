using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model class for anime/manga staff position.
/// </summary>
public class CharacterEntryResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; set; }

	/// <summary>
	/// Character's name.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Url to character's main page.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Character's set of images
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; set; }
}