using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Set of images in various formats.
/// </summary>
public class ImagesSetResponse

{
	/// <summary>
	/// Images in JPG format.
	/// </summary>
	[JsonPropertyName("jpg")]
	public ImageResponse? JPG { get; init; }

	/// <summary>
	/// Images in webp format.
	/// </summary>
	[JsonPropertyName("webp")]
	public ImageResponse? WebP { get; init; }
}