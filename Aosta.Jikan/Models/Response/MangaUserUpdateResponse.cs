using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Single manga user update model class.
/// </summary>
public class MangaUserUpdateResponse : UserUpdateResponse
{
	/// <summary>
	/// Amount of volumes read by the user.
	/// </summary>
	[JsonPropertyName("volumes_read")]
	public int? VolumesRead { get; init; }

	/// <summary>
	/// Total amount of the volumes.
	/// </summary>
	[JsonPropertyName("volumes_total")]
	public int? VolumesTotal { get; init; }

	/// <summary>
	/// Amount of chapters read by the user.
	/// </summary>
	[JsonPropertyName("chapters_read")]
	public int? ChaptersRead { get; init; }

	/// <summary>
	/// Total amount of the chapters.
	/// </summary>
	[JsonPropertyName("chapters_total")]
	public int? ChaptersTotal { get; init; }
}