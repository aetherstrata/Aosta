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
	public MangaListEntryDetailsResponse? Manga { get; init; }
}