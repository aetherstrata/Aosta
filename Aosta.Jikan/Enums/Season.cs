using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using FastEnumUtility;

namespace Aosta.Jikan.Enums;

/// <summary>
/// Enumeration representing seasons of year.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Season
{
    /// <summary>
    /// Spring season.
    /// </summary>
    [EnumMember(Value = "spring"), Label("Spring")]
    Spring,

    /// <summary>
    /// Summer season.
    /// </summary>
    [EnumMember(Value = "summer"), Label("Summer")]
    Summer,

    /// <summary>
    /// Fall season.
    /// </summary>
    [EnumMember(Value = "fall"), Label("Fall")]
    Fall,

    /// <summary>
    /// Winter season.
    /// </summary>
    [EnumMember(Value = "winter"), Label("Winter")]
    Winter
}
