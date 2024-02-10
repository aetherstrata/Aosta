using System.Runtime.Serialization;
using FastEnumUtility;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Enumeration for anime types (search config).
/// </summary>
public enum AnimeTypeFilter
{
    /// <summary>
    /// Do not filter results
    /// </summary>
    All,

	/// <summary>
	/// TV series.
	/// </summary>
	[EnumMember(Value = "tv")]
	[Label("TV")]
	TV,

	/// <summary>
	/// Original video animation.
	/// </summary>
	[EnumMember(Value = "ova")]
	[Label("OVA")]
	OVA,

	/// <summary>
	/// Feature-length movie.
	/// </summary>
	[EnumMember(Value = "movie")]
	[Label("movie")]
	Movie,

	/// <summary>
	/// A special episode.
	/// </summary>
	[EnumMember(Value = "special")]
	[Label("Special")]
	Special,

	/// <summary>
	/// Original net animation.
	/// </summary>
	[EnumMember(Value = "ona")]
	[Label("ONA")]
	ONA,

	/// <summary>
	/// Music video.
	/// </summary>
	[EnumMember(Value = "music")]
	[Label("Music")]
	Music,

    /// <summary>
    /// Commercial message.
    /// </summary>
    [EnumMember(Value = "cm")]
    [Label("Commercial Message")]
    CommercialMessage,

    /// <summary>
    /// Promotional video.
    /// </summary>
    [EnumMember(Value = "pv")]
    [Label("Promotional Video")]
    PromotionalVideo,

    /// <summary>
    /// TV Special.
    /// </summary>
    [EnumMember(Value = "tv_special")]
    [Label("TV Special")]
    TVSpecial
}
