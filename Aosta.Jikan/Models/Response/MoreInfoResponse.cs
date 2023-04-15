using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Extra information stored in "more info" tab.
/// </summary>
public class MoreInfoResponse
{
	/// <summary>
	/// Extra information stored in "more info" tab.
	/// </summary>
	[JsonPropertyName("moreinfo")]
	public string? Info { get; init; }
}