using Realms;

namespace Aosta.Data.Models.Embedded;

/// <summary>
/// Image model class.
/// </summary>
[Preserve(AllMembers = true)]
public partial class Image : IEmbeddedObject
{
	/// <summary>
	/// Url to default version of the image.
	/// </summary>
	public string? ImageUrl { get; set; }

	/// <summary>
	/// Url to small version of the image.
	/// </summary>
	public string? SmallImageUrl { get; set; }

	/// <summary>
	/// Url to medium version of the image.
	/// </summary>
	public string? MediumImageUrl { get; set; }

	/// <summary>
	/// Url to large version of the image.
	/// </summary>
	public string? LargeImageUrl { get; set; }

	/// <summary>
	/// Url to version of image with the biggest resolution.
	/// </summary>
	public string? MaximumImageUrl { get; set; }
}
