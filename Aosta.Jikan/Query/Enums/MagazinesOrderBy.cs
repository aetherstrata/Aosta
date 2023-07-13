using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Defines ways to order magazines.
/// </summary>
public enum MagazinesOrderBy
{
    /// <summary>
    /// Order by MyAnimeList ID.
    /// </summary>
    [EnumMember(Value = "mal_id")]
    MalId,

    /// <summary>
    /// Order by name of the magazine.
    /// </summary>
    [EnumMember(Value = "name")]
    Name,

    /// <summary>
    /// Order by entries count of the magazines.
    /// </summary>
    [EnumMember(Value = "count")]
    Count
}