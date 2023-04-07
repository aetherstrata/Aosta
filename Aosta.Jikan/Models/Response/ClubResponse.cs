using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Club profile model class.
/// </summary>
public class ClubResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; set; }

	/// <summary>
	/// Club's URL.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Club's image set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; set; }

	/// <summary>
	/// Name of the club.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Club's members count.
	/// </summary>
	[JsonPropertyName("members")]
	public int? MembersCount { get; set; }

	/// <summary>
	/// Club's category (Anime/Manga/Japan etc.)
	/// </summary>
	[JsonPropertyName("category")]
	public string? Category { get; set; }

	/// <summary>
	/// Club's access type (public/private).
	/// </summary>
	[JsonPropertyName("access")]
	public string? Access { get; set; }

	/// <summary>
	/// Club's date of creation.
	/// </summary>
	[JsonPropertyName("created")]
	public DateTimeOffset? Created { get; set; }
}