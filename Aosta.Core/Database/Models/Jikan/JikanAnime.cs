using Aosta.Core.Database.Models.Embedded;
using Aosta.Jikan.Enums;
using Realms;
using AiringStatus = Aosta.Core.Database.Enums.AiringStatus;
using Season = Aosta.Core.Database.Enums.Season;

namespace Aosta.Core.Database.Models.Jikan;

/// <summary>
/// Jikan anime model class.
/// </summary>
public partial class JikanAnime : IRealmObject
{
	private byte _season { get; set; }
	private byte _status { get; set; }
	private byte _type { get; set; }

	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[PrimaryKey]
	public long MalId { get; internal set; }

	/// <summary>
	/// Anime's canonical link.
	/// </summary>
	public string? Url { get; set; }

	/// <summary>
	/// Anime's images in various formats.
	/// </summary>
	public ImagesSet? Images { get; set; }

	/// <summary>
	/// Anime's trailer.
	/// </summary>
	public YouTubeVideo? Trailer { get; set; }

	/// <summary>
	/// Anime's multiple titles (if any).
	/// </summary>
	public IList<TitleEntry> Titles { get; }

	/// <summary>
	/// Anime type (e. g. "TV", "Movie").
	/// </summary>
	[Ignored]
	public AnimeType Type
	{
		get => (AnimeType)_type;
		set => _type = (byte)value;
	}

	/// <summary>
	/// Anime source (e .g. "Manga" or "Original").
	/// </summary>
	public string? Source { get; set; }

	/// <summary>
	/// Anime's episode count.
	/// </summary>
	public int Episodes { get; set; }

	/// <summary>
	/// Anime's airing status (e. g. "Currently Airing").
	/// </summary>
	[Ignored]
	public AiringStatus Status
	{
		get => (AiringStatus)_status;
		set => _status = (byte)value;

	}

	/// <summary>
	/// Is anime currently airing.
	/// </summary>
	public bool Airing { get; set; }

	/// <summary>
	/// Associative keys "from" and "to" which are alternative version of AiredString in ISO8601 format.
	/// </summary>
	public TimePeriod? Aired { get; set; }

	/// <summary>
	/// Anime's duration per episode.
	/// </summary>
	public string? Duration { get; set; }

	/// <summary>
	/// Anime's age rating.
	/// </summary>
	public string? Rating { get; set; }

	/// <summary>
	/// Anime's score on MyAnimeList up to 2 decimal places.
	/// </summary>
	public double? Score { get; set; }

	/// <summary>
	/// Number of people the anime has been scored by.
	/// </summary>
	public int ScoredBy { get; set; }

	/// <summary>
	/// Anime rank on MyAnimeList (score).
	/// </summary>
	public int Rank { get; set; }

	/// <summary>
	/// Anime popularity rank on MyAnimeList.
	/// </summary>
	public int Popularity { get; set; }

	/// <summary>
	/// Anime members count on MyAnimeList.
	/// </summary>
	public int Members { get; set; }

	/// <summary>
	/// Anime favourite count on MyAnimeList.
	/// </summary>
	public int Favorites { get; set; }

	/// <summary>
	/// Anime's synopsis.
	/// </summary>
	public string? Synopsis { get; set; }

	/// <summary>
	/// Anime's background info.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Season of the year the anime premiered.
	/// </summary>
	[Ignored]
	public Season Season
	{
		get => (Season)_season;
		set => _season = (byte)value;
	}

	/// <summary>
	/// Year the anime premiered.
	/// </summary>
	public int Year { get; set; }

	/// <summary>
	/// Anime broadcast day and timings (usually JST).
	/// </summary>
	public AnimeBroadcast? Broadcast { get; set; }

	/// <summary>
	/// Anime's producers numerically indexed with array values.
	/// </summary>
	public IList<MalUrl> Producers { get; } = null!;

	/// <summary>
	/// Anime's licensors numerically indexed with array values.
	/// </summary>
	public IList<MalUrl> Licensors { get; } = null!;

	/// <summary>
	/// Anime's studio(s) numerically indexed with array values.
	/// </summary>
	public IList<MalUrl> Studios { get; } = null!;

	/// <summary>
	/// Anime's genres numerically indexed with array values.
	/// </summary>
	public IList<MalUrl> Genres { get; } = null!;

	/// <summary>
	/// Explicit genres
	/// </summary>
	public IList<MalUrl> ExplicitGenres { get; } = null!;

	/// <summary>
	/// Anime's themes
	/// </summary>
	public IList<MalUrl> Themes { get; } = null!;

	/// <summary>
	/// Anime's demographics
	/// </summary>
	public IList<MalUrl> Demographics { get; } = null!;

	/// <summary>
	/// If Approved is false then this means the entry is still pending review on MAL.
	/// </summary>
	public bool Approved  { get; set; }

	/// <summary>
	/// Anime related entries.
	/// </summary>
	public IList<RelatedEntry> Relations { get; } = null!;

	/// <summary>
	/// Anime music themes (openings and endings).
	/// </summary>
	public AnimeThemes? MusicThemes { get; set; }

	/// <summary>
	/// Anime external links.
	/// </summary>
	public IList<ExternalLink> ExternalLinks { get; } = null!;

	/// <summary>
	/// Anime streaming links.
	/// </summary>
	public IList<ExternalLink> StreamingLinks { get; } = null!;
}
