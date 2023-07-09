using Realms;

namespace Aosta.Core.Database.Models.Embedded;

/// <summary>
/// Model representing anime openings and endings
/// </summary>
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
