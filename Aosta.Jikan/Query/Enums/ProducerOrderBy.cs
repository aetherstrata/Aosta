using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Defines ways to order producers.
/// </summary>
public enum ProducerOrderBy
{
    /// <summary>
    /// Order by MyAnimeList id
    /// </summary>
    [EnumMember(Value = "mal_id")]
    MalId,

    /// <summary>
    /// Order by productions count
    /// </summary>
    [EnumMember(Value = "count")]
    Count,

    /// <summary>
    /// Order by user favorites count
    /// </summary>
    [EnumMember(Value = "favorites")]
    Favorites,

    /// <summary>
    /// Order by establishment date
    /// </summary>
    [EnumMember(Value = "established")]
    Established
}