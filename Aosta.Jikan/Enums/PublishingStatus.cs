using System.Runtime.Serialization;

namespace Aosta.Jikan.Enums;

/// <summary>
/// Current status of manga (search config).
/// </summary>
public enum PublishingStatus
{
	/// <summary>
	/// Allow all statuses to be displayed in results.
	/// </summary>
	[EnumMember(Value = "")]
	EveryStatus,
		
	/// <summary>
	/// Publishing status.
	/// </summary>
	[EnumMember(Value = "publishing")]
	Publishing,

	/// <summary>
	/// Complete status.
	/// </summary>
	[EnumMember(Value = "complete")]
	Complete,

	/// <summary>
	/// Hiatus status.
	/// </summary>
	[EnumMember(Value = "hiatus")]
	Hiatus,

	/// <summary>
	/// Discontinued status.
	/// </summary>
	[EnumMember(Value = "discontinued")]
	Discontinued,

	/// <summary>
	/// Upcoming status.
	/// </summary>
	[EnumMember(Value = "upcoming")]
	Upcoming
}