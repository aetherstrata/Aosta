using Realms;

namespace Aosta.Data.Models.Embedded;

/// <summary>
/// Model class representing sub item on MyAnimeList without image.
/// </summary>
[Preserve(AllMembers = true)]
public partial class MalUrl : IEmbeddedObject
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	public long MalId { get; set; }

	/// <summary>
	/// Type of resource
	/// </summary>
	public string? Type { get; set; }

	/// <summary>
	/// Url to sub item main page.
	/// </summary>
	public string? Url { get; set; }

	/// <summary>
	/// Title/Name of the item
	/// </summary>
	public string? Name { get; set; }
}
