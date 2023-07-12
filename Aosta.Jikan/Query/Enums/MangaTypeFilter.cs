using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Enumeration for anime types (search config).
/// </summary>
public enum MangaTypeFilter
{
	/// <summary>
	/// Filter by manga
	/// </summary>
	[EnumMember(Value = "manga")]
	Manga,

	/// <summary>
	/// Filter by novel
	/// </summary>
	[EnumMember(Value = "novel")]
	Novel,

	/// <summary>
	/// Filter by light novel
	/// </summary>
	[EnumMember(Value = "lightnovel")]
	LightNovel,

	/// <summary>
	/// Filter by oneshot
	/// </summary>
	[EnumMember(Value = "oneshot")]
	OneShot,

	/// <summary>
	/// Filter by doujinshi
	/// </summary>
	[EnumMember(Value = "doujin")]
	Doujinshi,

	/// <summary>
	/// Filter by manhwa
	/// </summary>
	[EnumMember(Value = "manhwa")]
	Manhwa,

	/// <summary>
	/// Filter by manhua
	/// </summary>
	[EnumMember(Value = "manhua")]
	Manhua
}