using Realms;

namespace Aosta.Core.Database.Models.Embedded;

/// <summary>
/// Set of images in various formats.
/// </summary>
[Preserve(AllMembers = true)]
public partial class ImagesSet : IEmbeddedObject
{
	/// <summary>
	/// Images in JPG format.
	/// </summary>
	public Image? JPG { get; set; }

	/// <summary>
	/// Images in webp format.
	/// </summary>
	public Image? WebP { get; set; }
}
