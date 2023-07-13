using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Filter used to retrieve top reviews
/// </summary>
public enum TopReviewsTypeFilter
{
    /// <summary>
    /// Get top reviews for animes only
    /// </summary>
    [EnumMember(Value = "anime")]
    Anime,

    /// <summary>
    /// Get top reviews for mangas only
    /// </summary>
    [EnumMember(Value = "manga")]
    Manga
}
