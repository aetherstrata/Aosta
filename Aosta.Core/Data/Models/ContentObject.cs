using Aosta.Core.Data.Enums;
using Aosta.Core.Data.Models.Jikan;
using Realms;

namespace Aosta.Core.Data.Models;

#nullable disable

public partial class ContentObject : IRealmObject
{
    /// <summary>The unique ID of this content</summary>
    [PrimaryKey]
    public Guid Id { get; protected set; } = Guid.NewGuid();

    /// <summary>The online MyAnimeList metadata of this content</summary>
    public JikanContentObject JikanObject { get; set; }

    [Indexed]
    private int _Type { get; set; } = -1;

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
    public int EpisodeCount { get; set; } = 1;

    /// <summary>The list of episodes of this content</summary>
    /// <remarks>This is an inverse relationship link to <see cref="EpisodeObject"/>.<see cref="EpisodeObject.Content"/></remarks>
    //[Backlink(nameof(EpisodeObject.Content))]
    //public IQueryable<EpisodeObject> Episodes { get; }

    /// <summary>Content source (e .g. "Manga" or "Original").</summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>The score from 0 to 100</summary>
    [Indexed]
    public int Score { get; set; } = -1;

    /// <summary>The review of this content</summary>
    public string Review { get; set; } = string.Empty;

    private int _AiringStatus { get; set; } = -1;

    /// <summary>The airing status (e. g. "Currently Airing").</summary>
    public AiringStatus AiringStatus
    {
        get => (AiringStatus)_AiringStatus;
        set => _AiringStatus = (int)value;
    }
}
