using System.Text.Json.Serialization;
using Aosta.Jikan.Enums;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Entry on user's manga list model class.
/// </summary>
public class MangaListEntryResponse
{
	/// <summary>
	/// Current user's reading status of manga.
	/// </summary>
	[JsonPropertyName("reading_status")]
	public UserMangaReadingStatus ReadingStatus { get; init; }

	/// <summary>
	/// User's score for the manga. 0 if not assigned yet.
	/// </summary>
	[JsonPropertyName("score")]
	public int Score { get; init; }

	/// <summary>
	/// Manga's chapters count read by the user.
	/// </summary>
	[JsonPropertyName("chapters_read")]
	public int? ChaptersRead { get; init; }

	/// <summary>
	/// Manga's volumes count read by the user.
	/// </summary>
	[JsonPropertyName("volumes_read")]
	public int? VolumesRead { get; init; }

	/// <summary>
	/// Tags added by user.
	/// </summary>
	[JsonPropertyName("tags")]
	public string? Tags { get; init; }

	/// <summary>
	/// Does user reread manga.
	/// </summary>
	[JsonPropertyName("is_rereading")]
	public bool? IsRereading { get; init; }

	/// <summary>
	/// Start date of user reading.
	/// </summary>
	[JsonPropertyName("read_start_date")]
	public DateTimeOffset? ReadStartDate { get; init; }

	/// <summary>
	/// End date of user reading.
	/// </summary>
	[JsonPropertyName("read_end_date")]
	public DateTimeOffset? ReadEndDate { get; init; }

	/// <summary>
	/// Time user has been reading manga.
	/// </summary>
	[JsonPropertyName("days")]
	public int? Days { get; init; }

	/// <summary>
	/// Retail of manga on user's list.
	/// </summary>
	[JsonPropertyName("retail")]
	public int? Retail { get; init; }

	/// <summary>
	/// Priority of manga on user's list.
	/// </summary>
	[JsonPropertyName("priority")]
	public string? Priority { get; init; }

	/// <summary>
	/// Manga details.
	/// </summary>
	[JsonPropertyName("manga")]
	public MangaListEntryDetails? Manga { get; init; }
}

/// <summary>
/// Anime details on the user list.
/// </summary>
public class MangaListEntryDetails
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; init; }

	/// <summary>
	/// Title of the manga.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>
	/// Manga's URL
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Manga's image set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }

	/// <summary>
	/// Manga type (e. g. "Manga", "Light novel").
	/// </summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <summary>
	/// Manga's chapters total count. 0 if not finished.
	/// </summary>
	[JsonPropertyName("chapters")]
	public int? Chapters { get; init; }

	/// <summary>
	/// Manga's volumes total count. 0 if not finished.
	/// </summary>
	[JsonPropertyName("volumes")]
	public int? Volumes { get; init; }

	/// <summary>
	/// Anime's airing status (e. g. "Publishing").
	/// </summary>
	[JsonPropertyName("status")]
	public string? Status { get; init; }

	/// <summary>
	/// Is manga currently being published.
	/// </summary>
	[JsonPropertyName("publishing")]
	public bool Publishing { get; init; }

	/// <summary>
	/// Associative keys "from" and "to" .
	/// </summary>
	[JsonPropertyName("published")]
	public TimePeriodResponse? Published { get; init; }

	/// <summary>
	/// Manga's magazines numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("magazines")]
	public ICollection<MalUrlResponse>? Magazines { get; init; }

	/// <summary>
	/// Manga's genres numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("genres")]
	public ICollection<MalUrlResponse>? Genres { get; init; }

	/// <summary>
	/// Manga's demographics
	/// </summary>
	[JsonPropertyName("demographics")]
	public ICollection<MalUrlResponse>? Demographics { get; init; }
}