using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// User's profile model class.
/// </summary>
public class UserProfileResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long? MalId { get; set; }

	/// <summary>
	/// Username.
	/// </summary>
	[JsonPropertyName("username")]
	public string? Username { get; set; }

	/// <summary>
	/// User's URL
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// User's image set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; set; }

	/// <summary>
	/// User's gender.
	/// </summary>
	[JsonPropertyName("gender")]
	public string? Gender { get; set; }

	/// <summary>
	/// User's location.
	/// </summary>
	[JsonPropertyName("location")]
	public string? Location { get; set; }

	/// <summary>
	/// Timestamp of user's last activity.
	/// </summary>
	[JsonPropertyName("last_online")]
	public DateTimeOffset? LastOnline { get; set; }

	/// <summary>
	/// User's birthday.
	/// </summary>
	[JsonPropertyName("birthday")]
	public DateTimeOffset? Birthday { get; set; }

	/// <summary>
	/// Timestamp of user's account creation
	/// </summary>
	[JsonPropertyName("joined")]
	public DateTimeOffset? Joined { get; set; }
}