using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Magazine model class.
/// </summary>
public class MagazineResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; init; }

	/// <summary>
	/// Url to sub item main page.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Name of the magazine
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>
	/// Total count of manga assigned to this magazine
	/// </summary>
	[JsonPropertyName("count")]
	public int TotalCount { get; init; }
}