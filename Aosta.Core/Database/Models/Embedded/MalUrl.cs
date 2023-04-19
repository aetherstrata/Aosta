using Realms;

namespace Aosta.Core.Database.Models.Embedded;

/// <summary>
/// Model class representing sub item on MyAnimeList without image.
/// </summary>
public partial class MalUrl : IEmbeddedObject
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	public long MalId { get; init; }

	/// <summary>
	/// Type of resource
	/// </summary>
	public string? Type { get; init; }

	/// <summary>
	/// Url to sub item main page.
	/// </summary>
	public string? Url { get; init; }

	/// <summary>
	/// Title/Name of the item
	/// </summary>
	public string? Name { get; init; }
}
