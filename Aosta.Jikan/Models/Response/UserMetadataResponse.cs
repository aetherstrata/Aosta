using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model representing basic user metadata
/// </summary>
public class UserMetadataResponse
{
	/// <summary>
	/// Username.
	/// </summary>
	[JsonPropertyName("username")]
	public string? Username { get; init; }

	/// <summary>
	/// User's image URL
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }

	/// <summary>
	/// User's URL
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }
}