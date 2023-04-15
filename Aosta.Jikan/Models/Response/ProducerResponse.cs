using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Producer model class.
/// </summary>
public class ProducerResponse
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
	/// Names of the producer.
	/// </summary>
	[JsonPropertyName("titles")]
	public ICollection<TitleEntryResponse>? Titles { get; init; }

	/// <summary>
	/// Image URLs
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }

	/// <summary>
	/// Date of establishing.
	/// </summary>
	[JsonPropertyName("established")]
	public DateTimeOffset? Established { get; init; }

	/// <summary>
	/// Total count of anime assigned to this producer.
	/// </summary>
	[JsonPropertyName("count")]
	public int TotalCount { get; init; }

	/// <summary>
	/// About the producer
	/// </summary>
	[JsonPropertyName("about")]
	public string? About { get; init; }
}