using Aosta.Core.Data.Enums;
using Aosta.Core.Data.Ordering;
using Realms;

namespace Aosta.Core.Data.Models;

#nullable disable

public partial class ContentObject : IRealmObject
{
    #region Backing fields

    [Indexed]
    internal int _Type { get; private set; } = -1;

    [Indexed] 
    internal int _AiringStatus { get; private set; } = -1;

    [Indexed] 
    internal int _WatchStatus { get; private set; } = -1;

    internal int _Season { get; private set; } = 0;

    #endregion

    #region Properties

    /// <summary>The unique ID of this content</summary>
    [PrimaryKey]
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>The online MyAnimeList metadata of this content</summary>
    public JikanContentObject JikanResponseData { get; set; }

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

    //TODO: Modellare gli episodi
    /// <summary>The list of episodes of this content</summary>
    /// <remarks>This is an inverse relationship link to <see cref="EpisodeObject"/>.<see cref="EpisodeObject.Content"/></remarks>
    //[Backlink(nameof(EpisodeObject.Content))]
    //public IQueryable<EpisodeObject> Episodes { get; }

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
    public WatchStatus WatchStatus
    {
        get => (WatchStatus)_WatchStatus;
        set => _WatchStatus = (int)value;
    }

    /// <summary>Year the anime premiered.</summary>
    public int? Year { get; set; }

    /// <summary>Seasons of the year the anime premiered.</summary>
    [Ignored]
    public Seasons Season
    {
        get => (Seasons)_Season;
        set => _Season = (int)value;
    }

    #endregion

    public ContentObject() { }

    internal ContentObject(JikanContentObject jikanContent) : this()
    {
        UpdateFromJikan(jikanContent);
    }

    internal void UpdateFromJikan(JikanContentObject jikanContent)
    {
        JikanResponseData = jikanContent;
        Title = jikanContent.DefaultTitle;
        Type = jikanContent.Type;
        Source =jikanContent.Source;
        EpisodeCount = jikanContent.Episodes;
        AiringStatus = jikanContent.Status;
        Synopsis = jikanContent.Synopsis;
        Season = jikanContent.Season;
        Year = jikanContent.Year;
    }
}
