using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model class representing collection of related anime entries.
/// </summary>
public class RelatedEntryResponse
{
	/// <summary>
	/// Type of relation, e.g. "Adaptation" or "Side Story".
	/// </summary>
	[JsonPropertyName("relation")]
	public string? Relation { get; set; }

	/// <summary>
	/// Collection of related anime/manga of given relation type.
	/// </summary>
	[JsonPropertyName("entry")]
	public ICollection<MalUrlResponse>? Entry { get; set; }
}