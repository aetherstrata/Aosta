using System.Text.Json.Serialization;

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
	public long MalId { get; init; }

	/// <summary>
	/// Character's page url.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Character's name.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }
		
	/// <summary>
	/// Character's name in kanji.
	/// </summary>
	[JsonPropertyName("name_kanji")]
	public string? NameKanji { get; init; }

	/// <summary>
	/// Character's nicknames.
	/// </summary>
	[JsonPropertyName("nicknames")]
	public ICollection<string>? Nicknames { get; init; }

	/// <summary>
	/// About character
	/// </summary>
	[JsonPropertyName("about")]
	public string? About { get; init; }

	/// <summary>
	/// Character favourite count on MyAnimeList.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int? Favorites { get; init; }

	/// <summary>
	/// Character's image set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }
}