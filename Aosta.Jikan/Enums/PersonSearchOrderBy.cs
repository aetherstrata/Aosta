using System.Runtime.Serialization;

namespace Aosta.Jikan.Enums;

/// <summary>
/// Properties by which person search results can be ordered.
/// </summary>
public enum PersonSearchOrderBy
{
    /// <summary>
    /// Does not order results.
    /// </summary>
    [EnumMember(Value = "")]
    NoSorting,

    /// <summary>
    /// Orders results by MAL id.
    /// </summary>
    [EnumMember(Value = "mal_id")]
    MalId,

    /// <summary>
    /// Orders results by name.
    /// </summary>
    [EnumMember(Value = "name")]
    Name,

    /// <summary>
    /// Orders results by birthday.
    /// </summary>
    [EnumMember(Value = "birthday")]
    Birthday,

    /// <summary>
    /// Orders results by favorites.
    /// </summary>
    [EnumMember(Value = "favorites")]
    Favorites,
}