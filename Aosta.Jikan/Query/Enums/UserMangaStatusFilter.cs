using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Defines possible filters for a user manga list
/// </summary>
public enum UserMangaStatusFilter
{
    /// <summary>
    /// Do not filter entries
    /// </summary>
    [EnumMember(Value = "all")]
    All,

    /// <summary>
    /// Filter only mangas the user is currently reading
    /// </summary>
    [EnumMember(Value = "reading")]
    Reading,

    /// <summary>
    /// Filter only mangas the user has completed
    /// </summary>
    [EnumMember(Value = "completed")]
    Completed,

    /// <summary>
    /// Filter only mangas the user has put on hold
    /// </summary>
    [EnumMember(Value = "onhold")]
    OnHold,

    /// <summary>
    /// Filter only mangas the user has dropped
    /// </summary>
    [EnumMember(Value = "dropped")]
    Dropped,

    /// <summary>
    /// Filter only mangas the user plans to read
    /// </summary>
    [EnumMember(Value = "plantoread")]
    PlanToRead
}