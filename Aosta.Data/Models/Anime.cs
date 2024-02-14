using Aosta.Data.Database;
using Aosta.Data.Enums;
using Aosta.Data.Models.Embedded;

using Realms;

namespace Aosta.Data.Models;

/// Anime model class
[Preserve(AllMembers = true)]
public partial class Anime : IRealmObject, IHasPrimaryKey<Guid>
{
    #region Properties

    /// The unique ID of this content in the Realm
    [PrimaryKey]
    public Guid PrimaryKey { get; private set; } = Guid.NewGuid();

    /// The episodes of this content
    public IList<Episode> Episodes { get; } = null!;

    /// The score from 0 to 100
    [Indexed]
    public int? UserScore { get; set; }

    /// The review of this content
    public string Review { get; set; } = string.Empty;

    ///  The watch status (e. g. "Completed")
    public WatchingStatus WatchingStatus
    {
        get => (WatchingStatus)watchStatus;
        set => watchStatus = (int)value;
    }

    /// MyAnimeList ID of this anime.
    public long? ID { get; set; }

    /// Anime's canonical link.
    public string? Url { get; set; }

    /// Anime's images in various formats.
    public ImagesSet? Images { get; set; }

    /// Anime's trailer.
    public YouTubeVideo? Trailer { get; set; }

    /// Anime's multiple titles (if any).
    public IList<TitleEntry> Titles { get; } = null!;

    /// Anime type (e. g. "TV", "Movie").
    public ContentType Type
    {
        get => (ContentType)type;
        set => type = (byte)value;
    }

    /// Anime source (e .g. "Manga" or "Original").
    public string? Source { get; set; }

    /// Anime's airing status (e. g. "Currently Airing").
    public AiringStatus AiringStatus
    {
        get => (AiringStatus)airingStatus;
        set => airingStatus = (byte)value;
    }

    /// Is anime currently airing.
    public bool Airing { get; set; }

    /// Associative keys "from" and "to" which are alternative version of AiredString in ISO8601 format.
    public TimePeriod? Aired { get; set; }

    /// Anime's duration per episode.
    public string? Duration { get; set; }

    /// Anime's age rating.
    public AudienceRating Rating
    {
        get => (AudienceRating)rating;
        set => rating=(byte)value;
    }

    /// Anime's score on MyAnimeList up to 2 decimal places.
    public double? Score { get; set; }

    /// Number of people the anime has been scored by.
    public int ScoredBy { get; set; }

    /// Anime rank on MyAnimeList (score).
    public int Rank { get; set; }

    /// Anime popularity rank on MyAnimeList.
    public int Popularity { get; set; }

    /// Anime members count on MyAnimeList.
    public int Members { get; set; }

    /// Anime favourite count on MyAnimeList.
    public int Favorites { get; set; }

    /// Anime's synopsis.
    public string? Synopsis { get; set; }

    /// Anime's background info.
    public string? Background { get; set; }

    /// Season of the year the anime premiered.
    public Season Season
    {
        get => (Season)season;
        set => season = (byte)value;
    }

    /// Year the anime premiered.
    public int Year { get; set; }

    /// Anime broadcast day and timings (usually JST).
    public AnimeBroadcast? Broadcast { get; set; }

    /// Anime's producers numerically indexed with array values.
    public IList<MalUrl> Producers { get; } = null!;

    /// Anime's licensors numerically indexed with array values.
    public IList<MalUrl> Licensors { get; } = null!;

    /// Anime's studio(s) numerically indexed with array values.
    public IList<MalUrl> Studios { get; } = null!;

    /// Anime's genres numerically indexed with array values.
    public IList<MalUrl> Genres { get; } = null!;

    /// Explicit genres
    public IList<MalUrl> ExplicitGenres { get; } = null!;

    /// Anime's themes
    public IList<MalUrl> Themes { get; } = null!;

    /// Anime's demographics
    public IList<MalUrl> Demographics { get; } = null!;

    /// If Approved is false then this means the entry is still pending review on MAL.
    public bool Approved { get; set; }

    /// Anime related entries.
    public IList<RelatedEntry> Relations { get; } = null!;

    /// Anime music themes (openings and endings).
    public AnimeThemes? MusicThemes { get; set; }

    /// Anime external links.
    public IList<ExternalLink> ExternalLinks { get; } = null!;

    /// Anime streaming links.
    public IList<ExternalLink> StreamingLinks { get; } = null!;

    #endregion

    // Get the default title of this content
    public string GetDefaultTitle()
    {
        return Titles.First(x => x.Type == "Default").Title;
    }

    #region Backing properties

    [Indexed]
    [MapTo(nameof(AiringStatus))]
    private byte airingStatus { get; set; }

    [MapTo(nameof(Rating))]
    private byte rating { get; set; }

    [MapTo(nameof(Season))]
    private byte season { get; set; }

    [Indexed]
    [MapTo(nameof(Type))]
    private byte type { get; set; }

    [Indexed]
    [MapTo(nameof(WatchingStatus))]
    private int watchStatus { get; set; }

    #endregion

    public static readonly IComparer<Anime> TITLE_COMPARATOR = new AnimeTitleComparator();

    private sealed class AnimeTitleComparator : IComparer<Anime>
    {
        public int Compare(Anime? x, Anime? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;

            int titleCompare = string.Compare(x.GetDefaultTitle(), y.GetDefaultTitle(), StringComparison.Ordinal);

            return titleCompare != 0 ? titleCompare : x.ID.GetValueOrDefault().CompareTo(y.ID.GetValueOrDefault());
        }
    }
}
