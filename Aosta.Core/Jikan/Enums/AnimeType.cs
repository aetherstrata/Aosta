using System.ComponentModel;
using System.Runtime.Serialization;
using Aosta.Core.Exceptions;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Enumeration for anime types (search config).
/// </summary>
public enum AnimeType
{
	/// <summary>
	/// TV series.
	/// </summary>
	[Description("TV")]
	[EnumMember(Value = "tv")]
	TV,

	/// <summary>
	/// Original video animation.
	/// </summary>
	[Description("OVA")]
	[EnumMember(Value = "ova")]
	OVA,

	/// <summary>
	/// Feature-lenght movie.
	/// </summary>
	[Description("Movie")]
	[EnumMember(Value = "movie")]
	Movie,

	/// <summary>
	/// A special episode.
	/// </summary>
	[Description("Special")]
	[EnumMember(Value = "special")]
	Special,

	/// <summary>
	/// Original net animation.
	/// </summary>
	[Description("ONA")]
	[EnumMember(Value = "ona")]
	ONA,

	/// <summary>
	/// Music video.
	/// </summary>
	[Description("Music")]
	[EnumMember(Value = "music")]
	Music,

	/// <summary>
	/// Allow all types to be displayed in results.
	/// </summary>
	[Description("")]
	[EnumMember(Value = null)]
	EveryType
}

public static class AnimeTypeExtensions
{
	public static string ToStringCached(this AnimeType type) => type switch
	{
		AnimeType.TV => "TV",
		AnimeType.OVA => "OVA",
		AnimeType.Movie => "Movie",
		AnimeType.Special => "Special",
		AnimeType.ONA => "ONA",
		AnimeType.Music => "Music",
		AnimeType.EveryType => "",
		_ => throw new AostaInvalidEnumException<AnimeType>(type)
	};
}