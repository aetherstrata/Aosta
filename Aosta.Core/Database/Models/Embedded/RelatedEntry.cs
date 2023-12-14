using Realms;

namespace Aosta.Core.Database.Models.Embedded;

/// <summary>
/// Model class representing collection of related anime entries.
/// </summary>
[Preserve(AllMembers = true)]
public partial class RelatedEntry : IEmbeddedObject
{
	/// <summary>
	/// Type of relation, e.g. "Adaptation" or "Side Story".
	/// </summary>
	public string? Relation { get; set; }

	/// <summary>
	/// Collection of related anime/manga of given relation type.
	/// </summary>
	public IList<MalUrl> Entry { get; }
}
