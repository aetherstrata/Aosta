using System.Text.Json.Serialization;
using Aosta.Jikan.Enums;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Entry on user's anime list model class.
/// </summary>
public class AnimeListEntryResponse
{
	/// <summary>
	/// Current user's watching status of anime.
	/// </summary>
	[JsonPropertyName("watching_status")]
	public UserAnimeWatchingStatus WatchingStatus { get; init; }

	/// <summary>
	/// User's score for the anime. 0 if not assigned yet.
	/// </summary>
	[JsonPropertyName("score")]
	public int Score { get; init; }

	/// <summary>
	/// Anime's episodes count watched by the user.
	/// </summary>
	[JsonPropertyName("episodes_watched")]
	public int? EpisodesWatched { get; init; }

	/// <summary>
	/// Tags added by user.
	/// </summary>
	[JsonPropertyName("tags")]
	public string? Tags { get; init; }

	/// <summary>
	/// Does user rewatch anime.
	/// </summary>
	[JsonPropertyName("is_rewatching")]
	public bool? IsRewatching { get; init; }

	/// <summary>
	/// Start date of user watching.
	/// </summary>
	[JsonPropertyName("watch_start_date")]
	public DateTimeOffset? WatchStartDate { get; init; }

	/// <summary>
	/// End date of user watching.
	/// </summary>
	[JsonPropertyName("watch_end_date")]
	public DateTimeOffset? WatchEndDate { get; init; }

	/// <summary>
	/// Time user has been watching anime.
	/// </summary>
	[JsonPropertyName("days")]
	public int? Days { get; init; }

	/// <summary>
	/// Type of storage.
	/// </summary>
	[JsonPropertyName("storage")]
	public string? Storage { get; init; }

	/// <summary>
	/// Priority of anime on user's list.
	/// </summary>
	[JsonPropertyName("priority")]
	public string? Priority { get; init; }

	/// <summary>
	/// Anime details.
	/// </summary>
	[JsonPropertyName("anime")]
	public AnimeListEntryDetailsResponse? Anime { get; init; }
}
