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
	public UserAnimeWatchingStatus WatchingStatus { get; set; }

	/// <summary>
	/// User's score for the anime. 0 if not assigned yet.
	/// </summary>
	[JsonPropertyName("score")]
	public int Score { get; set; }

	/// <summary>
	/// Anime's episodes count watched by the user.
	/// </summary>
	[JsonPropertyName("episodes_watched")]
	public int? EpisodesWatched { get; set; }

	/// <summary>
	/// Tags added by user.
	/// </summary>
	[JsonPropertyName("tags")]
	public string? Tags { get; set; }

	/// <summary>
	/// Does user rewatch anime.
	/// </summary>
	[JsonPropertyName("is_rewatching")]
	public bool? IsRewatching { get; set; }

	/// <summary>
	/// Start date of user watching.
	/// </summary>
	[JsonPropertyName("watch_start_date")]
	public DateTimeOffset? WatchStartDate { get; set; }

	/// <summary>
	/// End date of user watching.
	/// </summary>
	[JsonPropertyName("watch_end_date")]
	public DateTimeOffset? WatchEndDate { get; set; }

	/// <summary>
	/// Time user has been watching anime.
	/// </summary>
	[JsonPropertyName("days")]
	public int? Days { get; set; }

	/// <summary>
	/// Type of storage.
	/// </summary>
	[JsonPropertyName("storage")]
	public string? Storage { get; set; }

	/// <summary>
	/// Priority of anime on user's list.
	/// </summary>
	[JsonPropertyName("priority")]
	public string? Priority { get; set; }

	/// <summary>
	/// Anime details.
	/// </summary>
	[JsonPropertyName("anime")]
	public AnimeListEntryDetailsResponse? Anime { get; set; }
}
