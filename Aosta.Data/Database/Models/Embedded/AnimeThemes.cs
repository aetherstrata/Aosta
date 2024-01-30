using Realms;

namespace Aosta.Data.Database.Models.Embedded;

/// <summary>
/// Model representing anime openings and endings
/// </summary>
[Preserve(AllMembers = true)]
public partial class AnimeThemes : IEmbeddedObject
{
	/// <summary>
	/// Anime openings.
	/// </summary>
	public IList<string> Openings { get; } = null!;

	/// <summary>
	/// Anime endings.
	/// </summary>
	public IList<string> Endings { get; } = null!;
}
