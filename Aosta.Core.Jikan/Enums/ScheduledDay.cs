using System.Runtime.Serialization;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Available options for schedule
/// </summary>
public enum ScheduledDay
{
	/// <summary>
	/// Unknown schedule.
	/// </summary>
	[EnumMember(Value = "unknown")]
	Unknown,

	/// <summary>
	/// Irregular airing.
	/// </summary>
	[EnumMember(Value = "other")]
	Other,

	/// <summary>
	/// Monday.
	/// </summary>
	[EnumMember(Value = "monday")]
	Monday,

	/// <summary>
	/// Tuesday.
	/// </summary>
	[EnumMember(Value = "tuesday")]
	Tuesday,

	/// <summary>
	/// Wednesday.
	/// </summary>
	[EnumMember(Value = "wednesday")]
	Wednesday,

	/// <summary>
	/// Thursday.
	/// </summary>
	[EnumMember(Value = "thursday")]
	Thursday,

	/// <summary>
	/// Friday.
	/// </summary>
	[EnumMember(Value = "friday")]
	Friday,

	/// <summary>
	/// Saturday.
	/// </summary>
	[EnumMember(Value = "saturday")]
	Saturday,

	/// <summary>
	/// Sunday.
	/// </summary>
	[EnumMember(Value = "sunday")]
	Sunday
}