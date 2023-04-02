using Aosta.Core.Data.Enums;
using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using Aosta.Core.Jikan.Models.Response;
using Realms;
using AiringStatus = Aosta.Core.Data.Enums.AiringStatus;

namespace Aosta.Core.Data.Models;

/// <summary> MyAnimeList data model </summary>
public partial class JikanContentObject : IRealmObject
{
    public JikanContentObject(AnimeResponse animeResponse) : this()
    {
        ArgumentNullException.ThrowIfNull(animeResponse.MalId, nameof(animeResponse.MalId));

        MalId = animeResponse.MalId.Value;
        Url = animeResponse.Url ?? string.Empty;
        Images = animeResponse.Images?.ToRealmObject();
        Trailer = animeResponse.Trailer?.ToRealmObject();
        Titles.AddRange(animeResponse.Titles);
        Type = ParseToContentType(animeResponse.Type ?? string.Empty);
        Source = animeResponse.Source ?? string.Empty;
        Episodes = animeResponse.Episodes;
        Status = ParseToAiringStatus(animeResponse.Status ?? string.Empty);
        Airing = animeResponse.Airing;
        Aired = animeResponse.Aired?.ToRealmObject();
        Duration = animeResponse.Duration ?? string.Empty; //TODO: implement parsing
        AgeRating = ParseToRating(animeResponse.Rating ?? string.Empty);
        Score = animeResponse.Score;
        ScoredBy = animeResponse.ScoredBy;
        Rank = animeResponse.Rank;
        Popularity = animeResponse.Popularity;
        Members = animeResponse.Members;
        Favorites = animeResponse.Favorites;
        Synopsis = animeResponse.Synopsis ?? string.Empty;
        Background = animeResponse.Background ?? string.Empty;
        Season = animeResponse.Season?.ToGroupEnum() ?? GroupSeason.None;
        Year = animeResponse.Year;
        Broadcast = animeResponse.Broadcast?.ToRealmObject();
        Producers.AddRange(animeResponse.Producers);
        Licensors.AddRange(animeResponse.Licensors);
        Studios.AddRange(animeResponse.Studios);
        Genres.AddRange(animeResponse.Genres);
        ExplicitGenres.AddRange(animeResponse.ExplicitGenres);
        Themes.AddRange(animeResponse.Themes);
        Demographics.AddRange(animeResponse.Demographics);
        Approved = animeResponse.Approved;
    }

    #region Backing fields

    private int _Type { get; set; } = -1;

    private int _Status { get; set; } = -1;

    private int _Rating { get; set; } = -1;

    private int _Season { get; set; } = 0;

    #endregion

    #region Properties

    /// <summary>ID associated with MyAnimeList.</summary>
    [PrimaryKey]
    public long MalId { get; private set; }

    /// <summary>Anime's canonical link.</summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>Anime's images in various formats.</summary>
    public ImageSetObject? Images { get; set; }

    /// <summary>Anime's trailer.</summary>
    public TrailerObject? Trailer { get; set; }

    /// <summary>Anime's multiple titles (if any).</summary>
    public IList<TitleObject> Titles { get; } = null!;

    /// <summary>Anime's default title </summary>
    [Ignored]
    public string DefaultTitle => Titles.First(x => x.Type.Equals("Default")).Title;

    /// <summary>Anime type (e. g. "TV", "Movie").</summary>
    [Ignored]
    public ContentType Type
    {
        get => (ContentType)_Type;
        set => _Type = (int)value;
    }

    /// <summary>Anime source (e .g. "Manga" or "Original").</summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>Anime's episode count.</summary>
    public int? Episodes { get; set; }

    /// <summary>Anime's airing status (e. g. "Currently Airing").</summary>
    [Ignored]
    public AiringStatus Status
    {
        get => (AiringStatus)_Status;
        set => _Status = (int)value;
    }

    /// <summary>Is anime currently airing.</summary>
    public bool Airing { get; set; }

    /// <summary>
    ///     Associative keys "from" and "to" which are alternative version of AiredString in ISO8601 format.
    /// </summary>
    public TimePeriodObject? Aired { get; set; }

    /// <summary>Anime's duration per episode.</summary>
    public string Duration { get; set; } = string.Empty;

    /// <summary>Anime's age rating.</summary>
    [Ignored]
    public AudienceRating AgeRating
    {
        get => (AudienceRating)_Rating;
        set => _Rating = (int)value;
    }

    /// <summary>Anime's score on MyAnimeList up to 2 decimal places.</summary>
    public double? Score { get; set; }

    /// <summary>Number of people the anime has been scored by.</summary>
    public int? ScoredBy { get; set; }

    /// <summary>Anime rank on MyAnimeList (score).</summary>
    public int? Rank { get; set; }

    /// <summary>Anime popularity rank on MyAnimeList.</summary>
    public int? Popularity { get; set; }

    /// <summary>Anime members count on MyAnimeList.</summary>
    public int? Members { get; set; }

    /// <summary>Anime favourite count on MyAnimeList.</summary>
    public int? Favorites { get; set; }

    /// <summary>Anime's synopsis.</summary>
    public string Synopsis { get; set; } = string.Empty;

    /// <summary>Anime's background info.</summary>
    public string Background { get; set; } = string.Empty;

    /// <summary>Seasons of the year the anime premiered.</summary>
    [Ignored]
    public GroupSeason Season
    {
        get => (GroupSeason)_Season;
        set => _Season = (int)value;
    }

    /// <summary>Year the anime premiered.</summary>
    public int? Year { get; set; }

    /// <summary>Anime broadcast day and timings (usually JST).</summary>
    public BroadcastObject? Broadcast { get; set; }

    /// <summary> Anime's producers numerically indexed with array values. </summary>
    public IList<UrlObject> Producers { get; } = null!;

    /// <summary> Anime's licensors numerically indexed with array values. </summary>
    public IList<UrlObject> Licensors { get; } = null!;

    /// <summary> Anime's studio(s) numerically indexed with array values. </summary>
    public IList<UrlObject> Studios { get; } = null!;

    /// <summary>Anime's genres numerically indexed with array values.</summary>
    public IList<UrlObject> Genres { get; } = null!;

    /// <summary>Explicit genres</summary>
    public IList<UrlObject> ExplicitGenres { get; } = null!;

    /// <summary>Anime's themes</summary>
    public IList<UrlObject> Themes { get; } = null!;

    /// <summary>Anime's demographics</summary>
    public IList<UrlObject> Demographics { get; } = null!;

    /// <summary> If Approved is false then this means the entry is still pending review on MAL. </summary>
    public bool Approved { get; set; }

    #endregion

    #region String parsers

    private static AudienceRating ParseToRating(string s) => s switch
    {
        "G - All Ages" => AudienceRating.Everyone,
        "PG - Children" => AudienceRating.Children,
        "PG-13 - Teens 13 or older" => AudienceRating.Teens,
        "R - 17+ (violence & profanity)" => AudienceRating.ViolenceProfanity,
        "R+ - Mild Nudity" => AudienceRating.MildNudity,
        "Rx - Hentai" => AudienceRating.Hentai,
        _ => AudienceRating.Unknown
    };


    private static AiringStatus ParseToAiringStatus(string s) => s switch
    {
        "Finished Airing" => AiringStatus.FinishedAiring,
        "Currently Airing" => AiringStatus.CurrentlyAiring,
        "Not yet aired" => AiringStatus.NotYetAired,
        _ => AiringStatus.NotAvailable
    };

    private static ContentType ParseToContentType(string s) => s switch
    {
        "Movie" => ContentType.Movie,
        "Music" => ContentType.Music,
        "ONA" => ContentType.ONA,
        "OVA" => ContentType.OVA,
        "Special" => ContentType.Special,
        "TV" => ContentType.TV,
        _ => ContentType.Unknown
    };

    #endregion
}