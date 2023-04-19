using System.Runtime.Serialization;
using FastEnumUtility;

namespace Aosta.Jikan.Enums;

/// <summary>
/// Enumeration for anime types.
/// </summary>
public enum AnimeType
{
    /// <summary>
    /// TV series.
    /// </summary>
    [EnumMember(Value = "tv")] [Label("TV")]
    TV,

    /// <summary>
    /// Original video animation.
    /// </summary>
    [EnumMember(Value = "ova")] [Label("OVA")]
    OVA,

    /// <summary>
    /// Feature-lenght movie.
    /// </summary>
    [EnumMember(Value = "Movie")] [Label("movie")]
    Movie,

    /// <summary>
    /// A special episode.
    /// </summary>
    [EnumMember(Value = "special")] [Label("Special")]
    Special,

    /// <summary>
    /// Original net animation.
    /// </summary>
    [EnumMember(Value = "ona")] [Label("ONA")]
    ONA,

    /// <summary>
    /// Music video.
    /// </summary>
    [EnumMember(Value = "music")] [Label("Music")]
    Music
}