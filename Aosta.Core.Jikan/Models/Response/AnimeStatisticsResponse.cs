﻿using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Anime related statistics model class.
/// </summary>
public class AnimeStatisticsResponse
{
	/// <summary>
	/// Number of users who labeled anime status as "watching"
	/// </summary>
	[JsonPropertyName("watching")]
	public int? Watching { get; set; }

	/// <summary>
	/// Number of users who labeled anime status as "completed"
	/// </summary>
	[JsonPropertyName("completed")]
	public int? Completed { get; set; }

	/// <summary>
	/// Number of users who labeled anime status as "on hold"
	/// </summary>
	[JsonPropertyName("on_hold")]
	public int? OnHold { get; set; }

	/// <summary>
	/// Number of users who labeled anime status as "dropped"
	/// </summary>
	[JsonPropertyName("dropped")]
	public int? Dropped { get; set; }

	/// <summary>
	/// Number of users who labeled anime status as "plan to watch"
	/// </summary>
	[JsonPropertyName("plan_to_watch")]
	public int? PlanToWatch { get; set; }

	/// <summary>
	/// Total count of users who added anime to their lists.
	/// </summary>
	[JsonPropertyName("total")]
	public int? Total { get; set; }

	/// <summary>
	/// Number of users who added anime to their lists.
	/// </summary>
	[JsonPropertyName("scores")]
	public ICollection<ScoringStatisticsResponse>? ScoreStats { get; set; }
}