using System.Runtime.Serialization;
using System.Text.Json.Serialization;

using FastEnumUtility;

namespace Aosta.Jikan.Enums;

/// <summary>
/// Enumeration for anime types.
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<AnimeType>))]
public enum AnimeType
{
    /// <summary>
    /// TV series.
    /// </summary>
    [EnumMember(Value = "TV"), Label("TV")]
    TV,

    /// <summary>
    /// TV special series.
    /// </summary>
    [EnumMember(Value = "TV Special"), Label("TV Special")]
    TVSpecial,

    /// <summary>
    /// Original video animation.
    /// </summary>
    [EnumMember(Value = "OVA"), Label("OVA")]
    OVA,

    /// <summary>
    /// Feature-length movie.
    /// </summary>
    [EnumMember(Value = "Movie"), Label("Movie")]
    Movie,

    /// <summary>
    /// A special episode.
    /// </summary>
    [EnumMember(Value = "Special"), Label("Special")]
    Special,

    /// <summary>
    /// Original net animation.
    /// </summary>
    [EnumMember(Value = "ONA"), Label("ONA")]
    ONA,

    /// <summary>
    /// Music video.
    /// </summary>
    [EnumMember(Value = "Music"), Label("Music")]
    Music,

    /// <summary>
    /// Promotional video.
    /// </summary>
    [EnumMember(Value = "PV"), Label("Promotional Video")]
    PromotionalVideo,

    /// <summary>
    /// Commercial message
    /// </summary>
    [EnumMember(Value = "CM"), Label("Commercial Message")]
    CommercialMessage
}
