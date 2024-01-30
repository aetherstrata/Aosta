using Realms;

namespace Aosta.Data.Database.Models.Embedded;

/// <summary>
/// Anime trailer model class.
/// </summary>
[Preserve(AllMembers = true)]
public partial class YouTubeVideo : IEmbeddedObject
{
	/// <summary>
	/// ID associated with Youtube.
	/// </summary>
	public string? YoutubeId { get; set; }

	/// <summary>
	/// Url to the video.
	/// </summary>
	public string? Url { get; set; }

	/// <summary>
	/// Embed url to the video.
	/// </summary>
	public string? EmbedUrl { get; set; }

	/// <summary>
	/// Image related to the trailer in various resolutions.
	/// </summary>
	public Image? Image { get; set; }
}
