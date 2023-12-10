using System.Runtime.Serialization;

namespace Aosta.Jikan.Enums;

public enum DaysOfWeek
{
    /// <summary>
    /// Aired on mondays
    /// </summary>
    [EnumMember(Value = "monday")]
    Mondays,

    /// <summary>
    /// Aired on tuesdays
    /// </summary>
    [EnumMember(Value = "tuesday")]
    Tuesdays,

    /// <summary>
    /// Aired on wednesdays
    /// </summary>
    [EnumMember(Value = "wednesday")]
    Wednesdays,

    /// <summary>
    /// Aired on thursdays
    /// </summary>
    [EnumMember(Value = "thursday")]
    Thursdays,

    /// <summary>
    /// Aired on fridays
    /// </summary>
    [EnumMember(Value = "friday")]
    Fridays,

    /// <summary>
    /// Aired on saturdays
    /// </summary>
    [EnumMember(Value = "saturday")]
    Saturdays,

    /// <summary>
    /// Aired on sundays
    /// </summary>
    [EnumMember(Value = "sunday")]
    Sundays,
}