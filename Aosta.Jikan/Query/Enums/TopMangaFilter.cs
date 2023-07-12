using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Filter used to retrieve top mangas
/// </summary>
public enum TopMangaFilter
{
    /// <summary>
    /// Filter by publishing
    /// </summary>
    [EnumMember(Value = "publishing")]
    Publishing,

    /// <summary>
    /// Filter by upcoming
    /// </summary>
    [EnumMember(Value = "upcoming")]
    Upcoming,

    /// <summary>
    /// Filter by popularity
    /// </summary>
    [EnumMember(Value = "bypopularity")]
    ByPopularity,

    /// <summary>
    /// Filter by favorite
    /// </summary>
    [EnumMember(Value = "favorite")]
    Favorite
}