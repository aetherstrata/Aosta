using Realms;

namespace Aosta.Data.Database.Models.Embedded;

/// <summary>
/// Model class for time periods, e. g. dates of start and end manga publishing.
/// </summary>
[Preserve(AllMembers = true)]
public partial class TimePeriod : IEmbeddedObject
{
	/// <summary>
	/// Start date.
	/// </summary>
	public DateTimeOffset? From { get; set; }

	/// <summary>
	/// End date.
	/// </summary>
	public DateTimeOffset? To { get; set; }
}
