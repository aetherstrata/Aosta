using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Defines possible type filters for user history.
/// </summary>
public enum UserHistoryTypeFilter
{
	/// <summary>
	/// Filter only history about anime.
	/// </summary>
	[EnumMember(Value = "anime")]
	Anime,

	/// <summary>
	/// Filter only history about manga.
	/// </summary>
	[EnumMember(Value = "manga")]
	Manga
}