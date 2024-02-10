using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Current status of anime or (search config).
/// </summary>
public enum AiringStatusFilter
{
    /// <summary>
    /// Do not filter results
    /// </summary>
    All,

    /// <summary>
    /// Airing status.
    /// </summary>
    [EnumMember(Value = "airing")]
    Airing,

    /// <summary>
    /// Complete status.
    /// </summary>
    [EnumMember(Value = "complete")]
    Complete,

    /// <summary>
    /// Upcoming status.
    /// </summary>
    [EnumMember(Value = "upcoming")]
    Upcoming
}
