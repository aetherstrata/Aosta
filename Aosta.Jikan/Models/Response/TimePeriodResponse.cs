using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model class for time periods, e. g. dates of start and end manga publishing.
/// </summary>
public class TimePeriodResponse
{
	/// <summary>
	/// Start date.
	/// </summary>
	[JsonPropertyName("from")]
	public DateTimeOffset? From { get; init; }

	/// <summary>
	/// End date.
	/// </summary>
	[JsonPropertyName("to")]
	public DateTimeOffset? To { get; init; }
}