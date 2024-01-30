using Aosta.Jikan.Enums;
using Realms;

namespace Aosta.Data.Database.Models.Embedded;

/// <summary>
/// Broadcast Details
/// </summary>
[Preserve(AllMembers = true)]
public partial class AnimeBroadcast : IEmbeddedObject
{
	private byte? day { get; set; }

	/// <summary>Day of the week</summary>
	[Ignored]
	public DaysOfWeek? Day
	{
		get => (DaysOfWeek?)day;
		set => day = (byte?)value;
	}

	/// <summary>Time in 24 hour format</summary>
	public DateTimeOffset? Time { get; set; }
}
