using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Enum for user gender.
/// </summary>
public enum UserGenderFilter
{
    /// <summary>
    /// Allow all types to be displayed in results.
    /// </summary>
    [EnumMember(Value = "any")]
    Any,

    /// <summary>
    /// Male gender.
    /// </summary>
    [EnumMember(Value = "male")]
    Male,

    /// <summary>
    /// Female gender.
    /// </summary>
    [EnumMember(Value = "female")]
    Female,

    /// <summary>
    /// Non-binary gender.
    /// </summary>
    [EnumMember(Value = "nonbinary")]
    NonBinary
}