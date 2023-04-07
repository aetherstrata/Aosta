using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model for mangaography entry of a person
/// </summary>
public class PersonMangaographyEntryResponse
{
	/// <summary>
	/// Person's mangaography entry.
	/// </summary>
	[JsonPropertyName("manga")]
	public MalImageSubItemResponse? Manga { get; set; }

	/// <summary>
	/// Position of the person in the manga production
	/// </summary>
	[JsonPropertyName("position")]
	public string? Position { get; set; }
}