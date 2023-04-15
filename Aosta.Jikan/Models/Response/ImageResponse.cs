using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Image model class.
/// </summary>
public class ImageResponse
{
	/// <summary>
	/// Url to default version of the image.
	/// </summary>
	[JsonPropertyName("image_url")]
	public string? ImageUrl { get; init; }

	/// <summary>
	/// Url to small version of the image.
	/// </summary>
	[JsonPropertyName("small_image_url")]
	public string? SmallImageUrl { get; init; }

	/// <summary>
	/// Url to medium version of the image.
	/// </summary>
	[JsonPropertyName("medium_image_url")]
	public string? MediumImageUrl { get; init; }

	/// <summary>
	/// Url to large version of the image.
	/// </summary>
	[JsonPropertyName("large_image_url")]
	public string? LargeImageUrl { get; init; }

	/// <summary>
	/// Url to version of image with the biggest resolution.
	/// </summary>
	[JsonPropertyName("maximum_image_url")]
	public string? MaximumImageUrl { get; init; }
}