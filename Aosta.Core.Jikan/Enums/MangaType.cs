using System.Runtime.Serialization;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Enumeration for anime types (search config).
/// </summary>
public enum MangaType
{
	/// <summary>
	/// Allow all types to be displayed in results.
	/// </summary>
	[EnumMember(Value = "")]
	EveryType,

	/// <summary>
	/// TV series.
	/// </summary>
	[EnumMember(Value = "manga")]
	Manga,

	/// <summary>
	/// Original video animation.
	/// </summary>
	[EnumMember(Value = "novel")]
	Novel,

	/// <summary>
	/// Feature-lenght movie.
	/// </summary>
	[EnumMember(Value = "oneshot")]
	OneShot,

	/// <summary>
	/// A special episode.
	/// </summary>
	[EnumMember(Value = "doujin")]
	Doujinshi,

	/// <summary>
	/// Original net animation.
	/// </summary>
	[EnumMember(Value = "manhwa")]
	Manhwa,

	/// <summary>
	/// Music video.
	/// </summary>
	[EnumMember(Value = "manhua")]
	Manhua
}