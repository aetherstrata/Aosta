using Aosta.Jikan.Enums;
using Realms;

namespace Aosta.Core.Database.Models.Embedded;

/// <summary>
/// Broadcast Details
/// </summary>
public partial class AnimeBroadcast : IEmbeddedObject
{
	[MapTo("Day")]
	private byte _day { get; set; }

	/// <summary>Day of the week</summary>
	[Ignored]
	public DaysOfWeek Day
	{
		get => (DaysOfWeek)_day;
		set => _day = (byte)value;
	}

	/// <summary>Time in 24 hour format</summary>
	public DateTimeOffset? Time { get; set; }

	/// <summary>Raw parsed broadcast string</summary>
	public string? String { get; set; }
}
