using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Enumeration representing seasons of year.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Season
{
    /// <summary>
    /// Spring season.
    /// </summary>
    [Description("spring")] Spring,

    /// <summary>
    /// Summer season.
    /// </summary>
    [Description("summer")] Summer,

    /// <summary>
    /// Fall season.
    /// </summary>
    [Description("fall")] Fall,

    /// <summary>
    /// Winter season.
    /// </summary>
    [Description("winter")] Winter
}

public static class SeasonExtensions
{
    public static string ToStringCached(this Season season) => season switch
    {
        Season.Spring => "spring",
        Season.Summer => "summer",
        Season.Fall => "fall",
        Season.Winter => "winter",
        _ => throw new UnreachableException()
    };
}