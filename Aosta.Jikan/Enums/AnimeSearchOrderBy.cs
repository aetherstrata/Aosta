using System.Runtime.Serialization;

namespace Aosta.Jikan.Enums;

/// <summary>
/// Properties by which anime search results can be ordered.
/// </summary>
public enum AnimeSearchOrderBy
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
	/// Orders results by score.
	/// </summary>
	[EnumMember(Value = "score")]
	Score,

	/// <summary>
	/// Orders results by anime type.
	/// </summary>
	[EnumMember(Value = "type")]
	Type,
		
	/// <summary>
	/// Orders results by count of scores.
	/// </summary>
	[EnumMember(Value = "scored_by")]
	ScoredBy,

	/// <summary>
	/// Orders results by members.
	/// </summary>
	[EnumMember(Value = "members")]
	Members,

	/// <summary>
	/// Orders results by favorites.
	/// </summary>
	[EnumMember(Value = "favorites")]
	Favorites,

	/// <summary>
	/// Orders results by MalId.
	/// </summary>
	[EnumMember(Value = "mal_id")]
	Id,

	/// <summary>
	/// Orders results by number of episodes.
	/// </summary>
	[EnumMember(Value = "episodes")]
	Episodes,

	/// <summary>
	/// Orders results by rating.
	/// </summary>
	[EnumMember(Value = "rating")]
	Rating,
		
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
	/// Orders results by rank.
	/// </summary>
	[EnumMember(Value = "rank")]
	Rank,
		
	/// <summary>
	/// Orders results by popularity.
	/// </summary>
	[EnumMember(Value = "popularity")]
	Popularity
}