using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Club related anime, manga nad characters.
/// </summary>
public class ClubRelationsResponse
{
	/// <summary>
	/// Club's anime relations.
	/// </summary>
	[JsonPropertyName("anime")]
	public ICollection<MalUrlResponse>? Anime { get; set; }

	/// <summary>
	/// Club's manga relations.
	/// </summary>
	[JsonPropertyName("manga")]
	public ICollection<MalUrlResponse>? Manga { get; set; }

	/// <summary>
	/// Club's character relations.
	/// </summary>
	[JsonPropertyName("characters")]
	public ICollection<MalUrlResponse>? Characters { get; set; }
}