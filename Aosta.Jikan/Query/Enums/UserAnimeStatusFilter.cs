using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Defines possible filters for a user anime list
/// </summary>
public enum UserAnimeStatusFilter
{
    /// <summary>
    /// Do not filter entries
    /// </summary>
    [EnumMember(Value = "all")]
    All,

    /// <summary>
    /// Filter only animes the user is currently watching
    /// </summary>
    [EnumMember(Value = "watching")]
    Watching,

    /// <summary>
    /// Filter only animes with completed status
    /// </summary>
    [EnumMember(Value = "completed")]
    Completed,

    /// <summary>
    /// Filter only animes that the user has put on hold
    /// </summary>
    [EnumMember(Value = "onhold")]
    OnHold,

    /// <summary>
    /// Filter only animes that the user has dropped
    /// </summary>
    [EnumMember(Value = "dropped")]
    Dropped,

    /// <summary>
    /// Filter only animes that the user plans to watch
    /// </summary>
    [EnumMember(Value = "plantowatch")]
    PlanToWatch
}