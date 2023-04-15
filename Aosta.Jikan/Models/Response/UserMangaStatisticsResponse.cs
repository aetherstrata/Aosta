using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// User's manga statistics model class.
/// </summary>
public class UserMangaStatisticsResponse
{
	/// <summary>
	/// Number of days user has been reading manga.
	/// </summary>
	[JsonPropertyName("days_read")]
	public decimal? DaysRead { get; init; }

	/// <summary>
	/// User's mean score for manga.
	/// </summary>
	[JsonPropertyName("mean_score")]
	public decimal? MeanScore { get; init; }

	/// <summary>
	/// User's amount of manga currently reading.
	/// </summary>
	[JsonPropertyName("reading")]
	public int? Reading { get; init; }

	/// <summary>
	/// User's amount of completed manga.
	/// </summary>
	[JsonPropertyName("completed")]
	public int? Completed { get; init; }

	/// <summary>
	/// User's amount of manga on hold.
	/// </summary>
	[JsonPropertyName("on_hold")]
	public int? OnHold { get; init; }

	/// <summary>
	/// User's amount of dropped manga.
	/// </summary>
	[JsonPropertyName("dropped")]
	public int? Dropped { get; init; }

	/// <summary>
	/// User's amount of plan to read manga.
	/// </summary>
	[JsonPropertyName("plan_to_read")]
	public int? PlanToRead { get; init; }

	/// <summary>
	/// User's total amount of manga.
	/// </summary>
	[JsonPropertyName("total_entries")]
	public int? TotalEntries { get; init; }

	/// <summary>
	/// Total times user reread manga.
	/// </summary>
	[JsonPropertyName("reread")]
	public int? Reread { get; init; }

	/// <summary>
	/// User's total amount of read chapters.
	/// </summary>
	[JsonPropertyName("chapters_read")]
	public int? ChaptersRead { get; init; }

	/// <summary>
	/// User's total amount of read volumes.
	/// </summary>
	[JsonPropertyName("volumes_read")]
	public int? VolumesRead { get; init; }
}