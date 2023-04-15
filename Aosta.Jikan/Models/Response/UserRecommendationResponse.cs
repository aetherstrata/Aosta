using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model class for single recommendation from user profile.
/// </summary>
public class UserRecommendationResponse
{
	/// <summary>
	/// Combined mal ids of both recommended entries.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public string? MalIds { get; init; }

	/// <summary>
	/// Both recommended entries/
	/// </summary>
	[JsonPropertyName("entry")]
	public ICollection<MalImageSubItemResponse>? Entries { get; init; }

	/// <summary>
	/// Recommendation content
	/// </summary>
	[JsonPropertyName("content")]
	public string? Content { get; init; }

	/// <summary>
	/// Date of creation
	/// </summary>
	[JsonPropertyName("date")]
	public DateTime Date { get; init; }

	/// <summary>
	/// Reviewing user.
	/// </summary>
	[JsonPropertyName("user")]
	public UserMetadataResponse? User { get; init; }
}