using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Filter used to retrieve top animes
/// </summary>
public enum TopAnimeFilter
{
	/// <summary>
	/// Filter by airing
	/// </summary>
	[EnumMember(Value = "airing")]
	Airing,

	/// <summary>
	/// Filter by upcoming
	/// </summary>
	[EnumMember(Value = "upcoming")]
	Upcoming,

	/// <summary>
	/// Filter by popularity
	/// </summary>
	[EnumMember(Value = "bypopularity")]
	ByPopularity,

	/// <summary>
	/// Filter by favorites
	/// </summary>
	[EnumMember(Value = "favorite")]
	Favorite
}