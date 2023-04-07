using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Club member model class.
/// </summary>
public class ClubMemberResponse
{
	/// <summary>
	/// Club member's Username.
	/// </summary>
	[JsonPropertyName("username")]
	public string? Username { get; set; }

	/// <summary>
	/// Club member's image set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; set; }

	/// <summary>
	/// Club member's URL
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }
}