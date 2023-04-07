using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Model for animeography entry of a person
/// </summary>
public class PersonAnimeographyEntryResponse
{
	/// <summary>
	/// Person's animeography entry.
	/// </summary>
	[JsonPropertyName("anime")]
	public MalImageSubItemResponse? Anime { get; set; }

	/// <summary>
	/// Position of the person in the anime production
	/// </summary>
	[JsonPropertyName("position")]
	public string? Position { get; set; }
}