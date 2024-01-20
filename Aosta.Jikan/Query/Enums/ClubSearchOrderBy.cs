using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Properties by which club search results can be ordered.
/// </summary>
public enum ClubSearchOrderBy
{
    /// <summary>
    /// Does not order results.
    /// </summary>
    [EnumMember(Value = "")]
    NoSorting,

    /// <summary>
    /// Orders results by title.
    /// </summary>
    [EnumMember(Value = "title")]
    Title,
		
    /// <summary>
    /// Orders results by MalId.
    /// </summary>
    [EnumMember(Value = "mal_id")]
    Id,

    /// <summary>
    /// Orders results by created date.
    /// </summary>
    [EnumMember(Value = "created")]
    Created,
		
    /// <summary>
    /// Orders results by count of members.
    /// </summary>
    [EnumMember(Value = "members_count")]
    MembersCount,
		
    /// <summary>
    /// Orders results by count of pictures.
    /// </summary>
    [EnumMember(Value = "pictures_count")]
    PicturesCount
}