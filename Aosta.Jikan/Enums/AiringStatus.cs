using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Aosta.Jikan.Enums;

/// <summary>
/// Current status of anime.
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<AiringStatus>))]
public enum AiringStatus
{
    /// <summary>
    /// Airing status.
    /// </summary>
    [EnumMember(Value = "Currently Airing")]
    Airing,

    /// <summary>
    /// Completed status.
    /// </summary>
    [EnumMember(Value = "Finished Airing")]
    Completed,

    /// <summary>
    /// Upcoming status.
    /// </summary>
    [EnumMember(Value = "Not yet aired")]
    Upcoming
}
