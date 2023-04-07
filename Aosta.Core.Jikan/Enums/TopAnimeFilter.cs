using System.Runtime.Serialization;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Defines filter used in top anime request
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