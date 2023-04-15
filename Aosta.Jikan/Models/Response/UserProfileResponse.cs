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
	public long? MalId { get; init; }

	/// <summary>
	/// Username.
	/// </summary>
	[JsonPropertyName("username")]
	public string? Username { get; init; }

	/// <summary>
	/// User's URL
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// User's image set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }

	/// <summary>
	/// User's gender.
	/// </summary>
	[JsonPropertyName("gender")]
	public string? Gender { get; init; }

	/// <summary>
	/// User's location.
	/// </summary>
	[JsonPropertyName("location")]
	public string? Location { get; init; }

	/// <summary>
	/// Timestamp of user's last activity.
	/// </summary>
	[JsonPropertyName("last_online")]
	public DateTimeOffset? LastOnline { get; init; }

	/// <summary>
	/// User's birthday.
	/// </summary>
	[JsonPropertyName("birthday")]
	public DateTimeOffset? Birthday { get; init; }

	/// <summary>
	/// Timestamp of user's account creation
	/// </summary>
	[JsonPropertyName("joined")]
	public DateTimeOffset? Joined { get; init; }
}