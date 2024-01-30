using Aosta.Data.Database.Models.Embedded;

using Realms;

namespace Aosta.Data.Database.Models.Jikan;

/// <summary>
/// Anime episode model class.
/// </summary>
[Preserve(AllMembers = true)]
public partial class JikanEpisode : IRealmObject
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[PrimaryKey]
	public long MalId { get; private set; }

	/// <summary>
	/// URL to the episode.
	/// </summary>
	public string? Url { get; set; }

	/// <summary>
	/// Titles of the episode.
	/// </summary>
	public IList<TitleEntry> Titles { get; } = null!;

	/// <summary>
	/// Episode's duration.
	/// </summary>
	public int? Duration { get; set; }

	/// <summary>
	/// Date when episode aired at first.
	/// </summary>
	public DateTimeOffset? Aired { get; set; }

	/// <summary>
	/// Is the episode filler.
	/// </summary>
	public bool? Filler { get; set; }

	/// <summary>
	/// Is the episode recap.
	/// </summary>
	public bool? Recap { get; set; }

	/// <summary>
	/// Episode's synopsis.
	/// </summary>
	public string? Synopsis { get; set; }

	/// <summary>
	/// URL to forum discussion
	/// </summary>
	public string? ForumUrl{ get; set; }
}