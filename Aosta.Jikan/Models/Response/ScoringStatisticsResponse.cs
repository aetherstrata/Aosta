using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model class representing number of votes and percentage share for single score.
/// </summary>
public class ScoringStatisticsResponse
{
	/// <summary>
	/// Score value (1-10).
	/// </summary>
	[JsonPropertyName("score")]
	public int Score { get; init; }

	/// <summary>
	/// Percentage share of overall votes poll.
	/// </summary>
	[JsonPropertyName("percentage")]
	public double? Percentage { get; init; }

	/// <summary>
	/// Number of votes casted for score.
	/// </summary>
	[JsonPropertyName("votes")]
	public int? Votes { get; init; }
}