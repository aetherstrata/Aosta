using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Single base user update model class.
/// </summary>
public class UserUpdateResponse
{
	/// <summary>
	/// Data about the user who made the update. Null if checked for specific user.
	/// </summary>
	[JsonPropertyName("user")]
	public UserMetadataResponse? User { get; init; }
		
	/// <summary>
	/// Data about the manga/anime the update. Null if checked for specific manga/anime.
	/// </summary>
	[JsonPropertyName("entry")]
	public MalImageSubItemResponse? Entry { get; init; }

	/// <summary>
	/// User's score.
	/// </summary>
	[JsonPropertyName("score")]
	public int? Score { get; init; }

	/// <summary>
	/// Date ofd the update.
	/// </summary>
	[JsonPropertyName("date")]
	public DateTimeOffset? Date { get; init; }

	/// <summary>
	/// Status (reading, watching, completed, etc.)
	/// </summary>
	[JsonPropertyName("status")]
	public string? Status { get; init; }
}