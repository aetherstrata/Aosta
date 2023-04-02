using System.ComponentModel;
using System.Runtime.Serialization;
using Aosta.Core.Exceptions;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Age rating for anime/manga (search config).
/// </summary>
public enum AnimeAgeRating
{
	/// <summary>
	/// All ages.
	/// </summary>
	[Description("g")]
	[EnumMember(Value = "G - All Ages")]
	G,

	/// <summary>
	/// Parental Guidance (Children).
	/// </summary>
	[Description("pg")]
	[EnumMember(Value = "PG - Children")]
	PG,

	/// <summary>
	/// Teens 13 or older.
	/// </summary>
	[Description("pg13")]
	[EnumMember(Value = "PG-13 - Teens 13 or older")]
	PG13,

	/// <summary>
	/// Rated 17 and above (Violence and Profanity).
	/// </summary>
	[Description("r17")]
	[EnumMember(Value = "R - 17+ (violence & profanity)")]
	R17,

	/// <summary>
	/// Mild nudity.
	/// </summary>
	[Description("r")]
	[EnumMember(Value = "R+ - Mild Nudity")]
	R,

	/// <summary>
	/// Adult (Hentai).
	/// </summary>
	[Description("rx")]
	[EnumMember(Value = "Rx - Hentai")]
	RX,

	/// <summary>
	/// All ages.
	/// </summary>
	[Description("")]
	[EnumMember(Value = null)]
	EveryRating
}

public static class AnimeAgeRatingExtensions
{
	public static string ToStringCached(this AnimeAgeRating ageRating) => ageRating switch
	{
		AnimeAgeRating.G => "g",
		AnimeAgeRating.PG => "pg",
		AnimeAgeRating.PG13 => "pg13",
		AnimeAgeRating.R17 => "r17",
		AnimeAgeRating.R => "r",
		AnimeAgeRating.RX => "rx",
		_ => throw new AostaInvalidEnumException<AnimeAgeRating>(ageRating)
	};
}