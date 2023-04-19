using System.Text.Json.Serialization;
using Aosta.Jikan.Converters;
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
	public long MalId { get; init; }

	/// <summary>
	/// Anime's canonical link.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Anime's images in various formats.
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }

	/// <summary>
	/// Anime's trailer.
	/// </summary>
	[JsonPropertyName("trailer")]
	public AnimeTrailerResponse? Trailer { get; init; }

	/// <summary>
	/// Title of the anime.
	/// </summary>
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>
	/// Title of the anime in English.
	/// </summary>
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	[JsonPropertyName("title_english")]
	public string? TitleEnglish { get; init; }

	/// <summary>
	/// Title of the anime in Japanese.
	/// </summary>
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	[JsonPropertyName("title_japanese")]
	public string? TitleJapanese { get; init; }

	/// <summary>
	/// Anime's multiple titles (if any). Return null if there is none.
	/// </summary>
	[Obsolete("This will be removed in the future. Use titles property instead.")]
	[JsonPropertyName("title_synonyms")]
	public ICollection<string>? TitleSynonyms { get; init; }

	/// <summary>
	/// Anime's multiple titles (if any).
	/// </summary>
	[JsonPropertyName("titles")]
	public ICollection<TitleEntryResponse>? Titles { get; init; }

	/// <summary>
	/// Anime type (e. g. "TV", "Movie").
	/// </summary>
	[JsonPropertyName("type")]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public AnimeType? Type { get; init; }

	/// <summary>
	/// Anime source (e .g. "Manga" or "Original").
	/// </summary>
	[JsonPropertyName("source")]
	public string? Source { get; init; }

	/// <summary>
	/// Anime's episode count.
	/// </summary>
	[JsonPropertyName("episodes")]
	public int? Episodes { get; init; }

	/// <summary>
	/// Anime's airing status (e. g. "Currently Airing").
	/// </summary>
	[JsonPropertyName("status")]
	[JsonConverter(typeof(AiringStatusEnumConverter))]
	public AiringStatus? Status { get; init; }

	/// <summary>
	/// Is anime currently airing.
	/// </summary>
	[JsonPropertyName("airing")]
	public bool Airing { get; init; }

	/// <summary>
	/// Associative keys "from" and "to" which are alternative version of AiredString in ISO8601 format.
	/// </summary>
	[JsonPropertyName("aired")]
	public TimePeriodResponse? Aired { get; init; }

	/// <summary>
	/// Anime's duration per episode.
	/// </summary>
	[JsonPropertyName("duration")]
	public string? Duration { get; init; }

	/// <summary>
	/// Anime's age rating.
	/// </summary>
	[JsonPropertyName("rating")]
	public string? Rating { get; init; }

	/// <summary>
	/// Anime's score on MyAnimeList up to 2 decimal places.
	/// </summary>
	[JsonPropertyName("score")]
	public double? Score { get; init; }

	/// <summary>
	/// Number of people the anime has been scored by.
	/// </summary>
	[JsonPropertyName("scored_by")]
	public int? ScoredBy { get; init; }

	/// <summary>
	/// Anime rank on MyAnimeList (score).
	/// </summary>
	[JsonPropertyName("rank")]
	public int? Rank { get; init; }

	/// <summary>
	/// Anime popularity rank on MyAnimeList.
	/// </summary>
	[JsonPropertyName("popularity")]
	public int? Popularity { get; init; }

	/// <summary>
	/// Anime members count on MyAnimeList.
	/// </summary>
	[JsonPropertyName("members")]
	public int? Members { get; init; }

	/// <summary>
	/// Anime favourite count on MyAnimeList.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int? Favorites { get; init; }

	/// <summary>
	/// Anime's synopsis.
	/// </summary>
	[JsonPropertyName("synopsis")]
	public string? Synopsis { get; init; }

	/// <summary>
	/// Anime's background info.
	/// </summary>
	[JsonPropertyName("background")]
	public string? Background { get; init; }

	/// <summary>
	/// Season of the year the anime premiered.
	/// </summary>
	[JsonPropertyName("season")]
	public Season? Season { get; init; }

	/// <summary>
	/// Year the anime premiered.
	/// </summary>
	[JsonPropertyName("year")]
	public int? Year { get; init; }

	/// <summary>
	/// Anime broadcast day and timings (usually JST).
	/// </summary>
	[JsonPropertyName("broadcast")]
	public AnimeBroadcastResponse? Broadcast { get; init; }

	/// <summary>
	/// Anime's producers numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("producers")]
	public ICollection<MalUrlResponse>? Producers { get; init; }

	/// <summary>
	/// Anime's licensors numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("licensors")]
	public ICollection<MalUrlResponse>? Licensors { get; init; }

	/// <summary>
	/// Anime's studio(s) numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("studios")]
	public ICollection<MalUrlResponse>? Studios { get; init; }

	/// <summary>
	/// Anime's genres numerically indexed with array values.
	/// </summary>
	[JsonPropertyName("genres")]
	public ICollection<MalUrlResponse>? Genres { get; init; }

	/// <summary>
	/// Explicit genres
	/// </summary>
	[JsonPropertyName("explicit_genres")]
	public ICollection<MalUrlResponse>? ExplicitGenres { get; init; }

	/// <summary>
	/// Anime's themes
	/// </summary>
	[JsonPropertyName("themes")]
	public ICollection<MalUrlResponse>? Themes { get; init; }

	/// <summary>
	/// Anime's demographics
	/// </summary>
	[JsonPropertyName("demographics")]
	public ICollection<MalUrlResponse>? Demographics { get; init; }

	/// <summary>
	/// If Approved is false then this means the entry is still pending review on MAL.
	/// </summary>
	[JsonPropertyName("approved")]
	public bool Approved  { get; init; }
}