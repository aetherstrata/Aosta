using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Manga model class.
/// </summary>
public class MangaResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; init; }

	/// <summary>
	/// Manga's canonical link.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Title of the manga.
	/// </summary>
	[JsonPropertyName("title")]
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	public string? Title { get; init; }

	/// <summary>
	/// Title of the manga in English.
	/// </summary>
	[JsonPropertyName("title_english")]
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	public string? TitleEnglish { get; init; }

	/// <summary>
	/// Title of the manga in English.
	/// </summary>
	[JsonPropertyName("title_japanese")]
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	public string? TitleJapanese { get; init; }

	/// <summary>
	/// Manga's multiple titles (if any). Return null if there is none.
	/// </summary>
	[JsonPropertyName("title_synonyms")]
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	public ICollection<string>? TitleSynonyms { get; init; }
		
	/// <summary>
	/// Anime's multiple titles (if any).
	/// </summary>
	[JsonPropertyName("titles")]
	public ICollection<TitleEntryResponse>? Titles { get; init; }

	/// <summary>
	/// Manga's images in various formats.
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }

	/// <summary>
	/// Manga's status (e. g. "Finished").
	/// </summary>
	[JsonPropertyName("status")]
	public string? Status { get; init; }

	/// <summary>
	/// Manga type (e. g. "Manga", "Light Novel").
	/// </summary>
	[JsonPropertyName("type")]
	public string? Type { get; init; }

	/// <summary>
	/// Manga's volume count.
	/// </summary>
	[JsonPropertyName("volumes")]
	public int? Volumes { get; init; }

	/// <summary>
	/// Manga's chapter count.
	/// </summary>
	[JsonPropertyName("chapters")]
	public int? Chapters { get; init; }

	/// <summary>
	/// Is manga currently being published.
	/// </summary>
	[JsonPropertyName("publishing")]
	public bool Publishing { get; init; }

	/// <summary>
	/// Associative keys "from" and "to" which are alternative version of PublishedString in ISO8601 format.
	/// </summary>
	[JsonPropertyName("published")]
	public TimePeriodResponse? Published { get; init; }

	/// <summary>
	/// Manga's score on MyAnimeList up to 2 decimal places.
	/// </summary>
	[JsonPropertyName("score")]
	public double? Score { get; init; }

	/// <summary>
	/// Number of people the manga has been scored by.
	/// </summary>
	[JsonPropertyName("scored_by")]
	public int? ScoredBy { get; init; }

	/// <summary>
	/// Manga rank on MyAnimeList (score).
	/// </summary>
	[JsonPropertyName("rank")]
	public int? Rank { get; init; }

	/// <summary>
	/// Manga popularity rank on MyAnimeList.
	/// </summary>
	[JsonPropertyName("popularity")]
	public int? Popularity { get; init; }

	/// <summary>
	/// Manga members count on MyAnimeList.
	/// </summary>
	[JsonPropertyName("members")]
	public int? Members { get; init; }

	/// <summary>
	/// Manga favourite count on MyAnimeList.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int? Favorites { get; init; }

	/// <summary>
	/// Manga's synopsis.
	/// </summary>
	[JsonPropertyName("synopsis")]
	public string? Synopsis { get; init; }

	/// <summary>
	/// Manga's background info. Return null if don't have any.
	/// </summary>
	[JsonPropertyName("background")]
	public string? Background { get; init; }

	/// <summary>
	/// Manga's genres numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("genres")]
	public ICollection<MalUrlResponse>? Genres { get; init; }

	/// <summary>
	/// Manga's authors numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("authors")]
	public ICollection<MalUrlResponse>? Authors { get; init; }

	/// <summary>
	/// Manga's serializations numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("serializations")]
	public ICollection<MalUrlResponse>? Serializations { get; init; }

	/// <summary>
	/// Explicit genres
	/// </summary>
	[JsonPropertyName("explicit_genres")]
	public ICollection<MalUrlResponse>? ExplicitGenres { get; init; }

	/// <summary>
	/// Manga's themes
	/// </summary>
	[JsonPropertyName("themes")]
	public ICollection<MalUrlResponse>? Themes { get; init; }

	/// <summary>
	/// Manga's demographics
	/// </summary>
	[JsonPropertyName("demographics")]
	public ICollection<MalUrlResponse>? Demographics { get; init; }
		
	/// <summary>
	/// If Approved is false then this means the entry is still pending review on MAL.
	/// </summary>
	[JsonPropertyName("approved")]
	public bool Approved  { get; init; }
}