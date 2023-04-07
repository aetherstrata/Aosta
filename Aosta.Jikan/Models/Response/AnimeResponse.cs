using System.Text.Json.Serialization;
using Aosta.Jikan.Enums;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Anime model class.
/// </summary>
public class AnimeResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long? MalId { get; set; }

	/// <summary>
	/// Anime's canonical link.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }

	/// <summary>
	/// Anime's images in various formats.
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; set; }

	/// <summary>
	/// Anime's trailer.
	/// </summary>
	[JsonPropertyName("trailer")]
	public AnimeTrailerResponse? Trailer { get; set; }

	/// <summary>
	/// Title of the anime.
	/// </summary>
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// Title of the anime in English.
	/// </summary>
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	[JsonPropertyName("title_english")]
	public string? TitleEnglish { get; set; }

	/// <summary>
	/// Title of the anime in Japanese.
	/// </summary>
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	[JsonPropertyName("title_japanese")]
	public string? TitleJapanese { get; set; }

	/// <summary>
	/// Anime's multiple titles (if any). Return null if there is none.
	/// </summary>
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	[JsonPropertyName("title_synonyms")]
	public ICollection<string>? TitleSynonyms { get; set; }

	/// <summary>
	/// Anime's multiple titles (if any).
	/// </summary>
	[JsonPropertyName("titles")]
	public ICollection<TitleEntryResponse>? Titles { get; set; }

	/// <summary>
	/// Anime type (e. g. "TV", "Movie").
	/// </summary>
	[JsonPropertyName("type")]
	public string? Type { get; set; }

	/// <summary>
	/// Anime source (e .g. "Manga" or "Original").
	/// </summary>
	[JsonPropertyName("source")]
	public string? Source { get; set; }

	/// <summary>
	/// Anime's episode count.
	/// </summary>
	[JsonPropertyName("episodes")]
	public int? Episodes { get; set; }

	/// <summary>
	/// Anime's airing status (e. g. "Currently Airing").
	/// </summary>
	[JsonPropertyName("status")]
	public string? Status { get; set; }

	/// <summary>
	/// Is anime currently airing.
	/// </summary>
	[JsonPropertyName("airing")]
	public bool Airing { get; set; }

	/// <summary>
	/// Assiociative keys "from" and "to" which are alternative version of AiredString in ISO8601 format.
	/// </summary>
	[JsonPropertyName("aired")]
	public TimePeriodResponse? Aired { get; set; }

	/// <summary>
	/// Anime's duration per episode.
	/// </summary>
	[JsonPropertyName("duration")]
	public string? Duration { get; set; }

	/// <summary>
	/// Anime's age rating.
	/// </summary>
	[JsonPropertyName("rating")]
	public string? Rating { get; set; }

	/// <summary>
	/// Anime's score on MyAnimeList up to 2 decimal places.
	/// </summary>
	[JsonPropertyName("score")]
	public double? Score { get; set; }

	/// <summary>
	/// Number of people the anime has been scored by.
	/// </summary>
	[JsonPropertyName("scored_by")]
	public int? ScoredBy { get; set; }

	/// <summary>
	/// Anime rank on MyAnimeList (score).
	/// </summary>
	[JsonPropertyName("rank")]
	public int? Rank { get; set; }

	/// <summary>
	/// Anime popularity rank on MyAnimeList.
	/// </summary>
	[JsonPropertyName("popularity")]
	public int? Popularity { get; set; }

	/// <summary>
	/// Anime members count on MyAnimeList.
	/// </summary>
	[JsonPropertyName("members")]
	public int? Members { get; set; }

	/// <summary>
	/// Anime favourite count on MyAnimeList.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int? Favorites { get; set; }

	/// <summary>
	/// Anime's synopsis.
	/// </summary>
	[JsonPropertyName("synopsis")]
	public string? Synopsis { get; set; }

	/// <summary>
	/// Anime's background info.
	/// </summary>
	[JsonPropertyName("background")]
	public string? Background { get; set; }

	/// <summary>
	/// Season of the year the anime premiered.
	/// </summary>
	[JsonPropertyName("season")]
	public Season? Season { get; set; }

	/// <summary>
	/// Year the anime premiered.
	/// </summary>
	[JsonPropertyName("year")]
	public int? Year { get; set; }

	/// <summary>
	/// Anime broadcast day and timings (usually JST).
	/// </summary>
	[JsonPropertyName("broadcast")]
	public AnimeBroadcastResponse? Broadcast { get; set; }

	/// <summary>
	/// Anime's producers numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("producers")]
	public ICollection<MalUrlResponse>? Producers { get; set; }

	/// <summary>
	/// Anime's licensors numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("licensors")]
	public ICollection<MalUrlResponse>? Licensors { get; set; }

	/// <summary>
	/// Anime's studio(s) numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("studios")]
	public ICollection<MalUrlResponse>? Studios { get; set; }

	/// <summary>
	/// Anime's genres numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("genres")]
	public ICollection<MalUrlResponse>? Genres { get; set; }

	/// <summary>
	/// Explicit genres
	/// </summary>
	[JsonPropertyName("explicit_genres")]
	public ICollection<MalUrlResponse>? ExplicitGenres { get; set; }

	/// <summary>
	/// Anime's themes
	/// </summary>
	[JsonPropertyName("themes")]
	public ICollection<MalUrlResponse>? Themes { get; set; }

	/// <summary>
	/// Anime's demographics
	/// </summary>
	[JsonPropertyName("demographics")]
	public ICollection<MalUrlResponse>? Demographics { get; set; }

	/// <summary>
	/// If Approved is false then this means the entry is still pending review on MAL.
	/// </summary>
	[JsonPropertyName("approved")]
	public bool Approved  { get; set; }

	/// <summary>
	/// Anime related entries.
	/// </summary>
	[JsonPropertyName("relations")]
	public ICollection<RelatedEntryResponse>? Relations { get; set; }

	/// <summary>
	/// Anime music themes (openings and endings).
	/// </summary>
	[JsonPropertyName("theme")]
	public AnimeThemesResponse? MusicThemes { get; set; }

	/// <summary>
	/// Anime external links.
	/// </summary>
	[JsonPropertyName("external")]
	public ICollection<ExternalLinkResponse>? ExternalLinks { get; set; }

	/// <summary>
	/// Anime streaming links.
	/// </summary>
	[JsonPropertyName("streaming")]
	public ICollection<ExternalLinkResponse>? StreamingLinks { get; set; }
}