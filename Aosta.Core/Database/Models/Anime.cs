using Aosta.Core.Database.Enums;
using Aosta.Core.Database.Models.Jikan;
using Realms;

namespace Aosta.Core.Database.Models;

/// <summary>
/// Anime model class
/// </summary>
[Preserve(AllMembers = true)]
public partial class Anime : IRealmObject, IHasGuidPrimaryKey
{
    #region Backing fields

    [Indexed]
    internal int _Type { get; private set; } = -1;

    [Indexed]
    internal int _AiringStatus { get; private set; } = -1;

    [Indexed]
    internal int _WatchStatus { get; private set; } = -1;

    internal int _Season { get; private set; } = 0;

    #endregion Backing Fields

    #region Properties

    /// <summary>The unique ID of this content</summary>
    [PrimaryKey]
    public Guid ID { get; private set; } = Guid.NewGuid();

    /// <summary>The online MyAnimeList metadata of this content</summary>
    public JikanAnime? Jikan { get; set; }

    /// <summary>The type of this content. (Eg. Movie, OVA)</summary>
    public ContentType Type
    {
        get => (ContentType)_Type;
        set => _Type = (int)value;
    }

    /// <summary>The title of this content</summary>
    [Indexed]
    public string Title { get; set; } = string.Empty;

    /// <summary>The description of this content</summary>
    public string Synopsis { get; set; } = string.Empty;

    /// <summary>The episode count of this content</summary>
    public int? EpisodeCount { get; set; }

    /// <summary>The episodes of this content</summary>
    public IList<Episode> Episodes { get; }

    /// <summary>Content source (e .g. "Manga" or "Original").</summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>The score from 0 to 100</summary>
    [Indexed]
    public int? Score { get; set; }

    /// <summary>The review of this content</summary>
    public string Review { get; set; } = string.Empty;

    /// <summary>The airing status (e. g. "Currently Airing").</summary>
    [Ignored]
    public AiringStatus AiringStatus
    {
        get => (AiringStatus)_AiringStatus;
        set => _AiringStatus = (int)value;
    }

    /// <summary> The watch status (e. g. "Completed")</summary>
    [Ignored]
    public WatchingStatus WatchingStatus
    {
        get => (WatchingStatus)_WatchStatus;
        set => _WatchStatus = (int)value;
    }

    /// <summary>Year the anime premiered.</summary>
    public int? Year { get; set; }

    /// <summary>Seasons of the year the anime premiered.</summary>
    [Ignored]
    public GroupSeason GroupSeason
    {
        get => (GroupSeason)_Season;
        set => _Season = (int)value;
    }
    
    public User? User { get; set; }

    #endregion

}