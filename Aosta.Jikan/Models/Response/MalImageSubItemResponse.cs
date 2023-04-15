using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model class representing sub item on MyAnimeList with image.
/// </summary>
public class MalImageSubItemResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; init; }

	/// <summary>
	/// Item's name.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>
	/// Item's title.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>
	/// Url to sub item main page.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Item's images set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }
}