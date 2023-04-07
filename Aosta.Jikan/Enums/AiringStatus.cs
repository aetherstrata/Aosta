using System.Runtime.Serialization;

namespace Aosta.Jikan.Enums;

/// <summary>
/// Current status of anime or (search config).
/// </summary>
public enum AiringStatus
{
    /// <summary>
    /// Allow all statuses to be displayed in results.
    /// </summary>
    [EnumMember(Value = "")]
    EveryStatus,

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
