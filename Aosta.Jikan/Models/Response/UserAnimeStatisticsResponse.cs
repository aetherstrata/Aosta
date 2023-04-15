using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// User's anime statistics model class.
/// </summary>
public class UserAnimeStatisticsResponse
{
	/// <summary>
	/// Number of days user has been watching anime.
	/// </summary>
	[JsonPropertyName("days_watched")]
	public decimal? DaysWatched { get; init; }

	/// <summary>
	/// User's mean score for anime.
	/// </summary>
	[JsonPropertyName("mean_score")]
	public decimal? MeanScore { get; init; }

	/// <summary>
	/// User's amount of anime currently watching.
	/// </summary>
	[JsonPropertyName("watching")]
	public int? Watching { get; init; }

	/// <summary>
	/// User's amount of completed anime.
	/// </summary>
	[JsonPropertyName("completed")]
	public int? Completed { get; init; }

	/// <summary>
	/// User's amount of anime on hold.
	/// </summary>
	[JsonPropertyName("on_hold")]
	public int? OnHold { get; init; }

	/// <summary>
	/// User's amount of dropped anime.
	/// </summary>
	[JsonPropertyName("dropped")]
	public int? Dropped { get; init; }

	/// <summary>
	/// User's amount of plan to watch anime.
	/// </summary>
	[JsonPropertyName("plan_to_watch")]
	public int? PlanToWatch { get; init; }

	/// <summary>
	/// User's total amount of anime.
	/// </summary>
	[JsonPropertyName("total_entries")]
	public int? TotalEntries { get; init; }

	/// <summary>
	/// Total times user rewatched anime.
	/// </summary>
	[JsonPropertyName("rewatched")]
	public int? Rewatched { get; init; }

	/// <summary>
	/// User's total amount of watched episodes.
	/// </summary>
	[JsonPropertyName("episodes_watched")]
	public int? EpisodesWatched { get; init; }
}