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
	public long MalId { get; init; }

	/// <summary>
	/// Club's URL.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Club's image set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }

	/// <summary>
	/// Name of the club.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>
	/// Club's members count.
	/// </summary>
	[JsonPropertyName("members")]
	public int? MembersCount { get; init; }

	/// <summary>
	/// Club's category (Anime/Manga/Japan etc.)
	/// </summary>
	[JsonPropertyName("category")]
	public string? Category { get; init; }

	/// <summary>
	/// Club's access type (public/private).
	/// </summary>
	[JsonPropertyName("access")]
	public string? Access { get; init; }

	/// <summary>
	/// Club's date of creation.
	/// </summary>
	[JsonPropertyName("created")]
	public DateTimeOffset? Created { get; init; }
}