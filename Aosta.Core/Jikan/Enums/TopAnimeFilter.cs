using System.ComponentModel;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Defines filter used in top anime request
/// </summary>
public enum TopAnimeFilter
{
	/// <summary>
	/// Filter by airing
	/// </summary>
	[Description("airing")]
	Airing,

	/// <summary>
	/// Filter by upcoming
	/// </summary>
	[Description("upcoming")]
	Upcoming,

	/// <summary>
	/// Filter by popularity
	/// </summary>
	[Description("bypopularity")]
	ByPopularity,

	/// <summary>
	/// Filter by favorites
	/// </summary>
	[Description("favorite")]
	Favorite
}