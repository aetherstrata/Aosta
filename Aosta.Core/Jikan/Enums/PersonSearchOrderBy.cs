using System.ComponentModel;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Properties by which person search results can be ordered.
/// </summary>
public enum PersonSearchOrderBy
{
    /// <summary>
    /// Does not order results.
    /// </summary>
    [Description("")]
    NoSorting,

    /// <summary>
    /// Orders results by MAL id.
    /// </summary>
    [Description("mal_id")]
    MalId,

    /// <summary>
    /// Orders results by name.
    /// </summary>
    [Description("name")]
    Name,

    /// <summary>
    /// Orders results by birthday.
    /// </summary>
    [Description("birthday")]
    Birthday,

    /// <summary>
    /// Orders results by favorites.
    /// </summary>
    [Description("favorites")]
    Favorites,
}