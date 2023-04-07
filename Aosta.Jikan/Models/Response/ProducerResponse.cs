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
	public long MalId { get; set; }

	/// <summary>
	/// Url to sub item main page.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Names of the producer.
	/// </summary>
	[JsonPropertyName("titles")]
	public ICollection<TitleEntryResponse>? Titles { get; set; }

	/// <summary>
	/// Image URLs
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; set; }

	/// <summary>
	/// Date of establishing.
	/// </summary>
	[JsonPropertyName("established")]
	public DateTime? Established { get; set; }

	/// <summary>
	/// Total count of anime assigned to this producer.
	/// </summary>
	[JsonPropertyName("count")]
	public int TotalCount { get; set; }

	/// <summary>
	/// About the producer
	/// </summary>
	[JsonPropertyName("about")]
	public string? About { get; set; }
}