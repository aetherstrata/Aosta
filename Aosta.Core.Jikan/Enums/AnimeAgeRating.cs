using System.Runtime.Serialization;
using FastEnumUtility;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Age rating for anime/manga (search config).
/// </summary>
public enum AnimeAgeRating
{
	/// <summary>
	/// All ages.
	/// </summary>
	[EnumMember(Value = "g")]
	[Label("G - All Ages")]
	G,

	/// <summary>
	/// Parental Guidance (Children).
	/// </summary>
	[EnumMember(Value = "pg")]
	[Label("PG - Children")]
	PG,

	/// <summary>
	/// Teens 13 or older.
	/// </summary>
	[EnumMember(Value = "pg13")]
	[Label("PG-13 - Teens 13 or older")]
	PG13,

	/// <summary>
	/// Rated 17 and above (Violence and Profanity).
	/// </summary>
	[EnumMember(Value = "r17")]
	[Label("R - 17+ (violence & profanity)")]
	R17,

	/// <summary>
	/// Mild nudity.
	/// </summary>
	[EnumMember(Value = "r")]
	[Label("R+ - Mild Nudity")]
	R,

	/// <summary>
	/// Adult (Hentai).
	/// </summary>
	[EnumMember(Value = "rx")]
	[Label("Rx - Hentai")]
	RX,

	/// <summary>
	/// All ages.
	/// </summary>
	[EnumMember(Value = "")]
	EveryRating
}
