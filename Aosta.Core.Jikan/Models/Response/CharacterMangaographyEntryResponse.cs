using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Model for character mangaography
/// </summary>
public class CharacterMangaographyEntryResponse
{
	/// <summary>
	/// Character's animeography entry.
	/// </summary>
	[JsonPropertyName("manga")]
	public MalImageSubItemResponse? Manga { get; set; }

	/// <summary>
	/// Role of character in sub item (anime or manga). Not available in all requests.
	/// </summary>
	[JsonPropertyName("role")]
	public string? Role { get; set; }
}