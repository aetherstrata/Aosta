using System.Runtime.Serialization;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Enumeration with possible extensions for User history request.
/// </summary>
public enum UserHistoryExtension
{
	/// <summary>
	/// Basic extension, parse data for both anime and manga history.
	/// </summary>
	[EnumMember(Value = "")]
	Both,

	/// <summary>
	/// Anime extension, parses only history about anime.
	/// </summary>
	[EnumMember(Value = "anime")]
	Anime,

	/// <summary>
	/// Manga extension, parses only history about manga.
	/// </summary>
	[EnumMember(Value = "manga")]
	Manga
}