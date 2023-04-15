using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Anime related statistics model class.
/// </summary>
public class AnimeStatisticsResponse
{
	/// <summary>
	/// Number of users who labeled anime status as "watching"
	/// </summary>
	[JsonPropertyName("watching")]
	public int? Watching { get; init; }

	/// <summary>
	/// Number of users who labeled anime status as "completed"
	/// </summary>
	[JsonPropertyName("completed")]
	public int? Completed { get; init; }

	/// <summary>
	/// Number of users who labeled anime status as "on hold"
	/// </summary>
	[JsonPropertyName("on_hold")]
	public int? OnHold { get; init; }

	/// <summary>
	/// Number of users who labeled anime status as "dropped"
	/// </summary>
	[JsonPropertyName("dropped")]
	public int? Dropped { get; init; }

	/// <summary>
	/// Number of users who labeled anime status as "plan to watch"
	/// </summary>
	[JsonPropertyName("plan_to_watch")]
	public int? PlanToWatch { get; init; }

	/// <summary>
	/// Total count of users who added anime to their lists.
	/// </summary>
	[JsonPropertyName("total")]
	public int? Total { get; init; }

	/// <summary>
	/// Number of users who added anime to their lists.
	/// </summary>
	[JsonPropertyName("scores")]
	public ICollection<ScoringStatisticsResponse>? ScoreStats { get; init; }
}