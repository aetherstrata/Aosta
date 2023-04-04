using System.ComponentModel;
using Aosta.Core.Exceptions;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Current status of anime or (search config).
/// </summary>
public enum AiringStatus
{
    /// <summary>
    /// Allow all statuses to be displayed in results.
    /// </summary>
    [Description("")]
    EveryStatus,

    /// <summary>
    /// Airing status.
    /// </summary>
    [Description("airing")]
    Airing,

    /// <summary>
    /// Complete status.
    /// </summary>
    [Description("complete")]
    Complete,

    /// <summary>
    /// Upcoming status.
    /// </summary>
    [Description("upcoming")]
    Upcoming
}

public static class AiringStatusExtensions
{
    public static string ToStringCached(this AiringStatus airingStatus) => airingStatus switch
    {
        AiringStatus.EveryStatus => "",
        AiringStatus.Airing => "airing",
        AiringStatus.Complete => "complete",
        AiringStatus.Upcoming => "upcoming",
        _ => throw new AostaInvalidEnumException<AiringStatus>(airingStatus)
    };
}