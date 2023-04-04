using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Model class for entry on user's history (single update).
/// </summary>
public class HistoryEntryResponse
{
	/// <summary>
	/// Metadata about updated manga/anime.
	/// </summary>
	[JsonPropertyName("meta")]
	public MalUrlResponse? Metadata { get; set; }

	/// <summary>
	/// New value for watched episodes/read chapters.
	/// </summary>
	[JsonPropertyName("increment")]
	public int Increment { get; set; }

	/// <summary>
	/// Date of the update.
	/// </summary>
	[JsonPropertyName("date")]
	public DateTime? Date { get; set; }
}