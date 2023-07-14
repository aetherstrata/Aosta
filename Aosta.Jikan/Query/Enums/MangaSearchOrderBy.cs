using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Properties by which manga search results can be ordered.
/// </summary>
public enum MangaSearchOrderBy
{
	/// <summary>
	/// Does not order results.
	/// </summary>
	[EnumMember(Value = "")]
	NoSorting,

	/// <summary>
	/// Orders results by title.
	/// </summary>
	[EnumMember(Value = "title")]
	Title,

	/// <summary>
	/// Orders results by start date.
	/// </summary>
	[EnumMember(Value = "start_date")]
	StartDate,

	/// <summary>
	/// Orders results by end date.
	/// </summary>
	[EnumMember(Value = "end_date")]
	EndDate,

	/// <summary>
	/// Orders results by score.
	/// </summary>
	[EnumMember(Value = "score")]
	Score,
		
	/// <summary>
	/// Orders results by count of scores.
	/// </summary>
	[EnumMember(Value = "scored_by")]
	ScoredBy,

	/// <summary>
	/// Orders results by anime type.
	/// </summary>
	[EnumMember(Value = "type")]
	Type,

	/// <summary>
	/// Orders results by members.
	/// </summary>
	[EnumMember(Value = "members")]
	Members,

	/// <summary>
	/// Orders results by id.
	/// </summary>
	[EnumMember(Value = "mal_id")]
	MalId,

	/// <summary>
	/// Orders results by number of chapters.
	/// </summary>
	[EnumMember(Value = "chapters")]
	Chapters,

	/// <summary>
	/// Orders results by number of volumes.
	/// </summary>
	[EnumMember(Value = "volumes")]
	Volumes,

	/// <summary>
	/// Orders results by rank.
	/// </summary>
	[EnumMember(Value = "rank")]
	Rank,
		
	/// <summary>
	/// Orders results by popularity.
	/// </summary>
	[EnumMember(Value = "popularity")]
	Popularity,

	/// <summary>
	/// Orders results by favorites.
	/// </summary>
	[EnumMember(Value = "favorites")]
	Favorites
}