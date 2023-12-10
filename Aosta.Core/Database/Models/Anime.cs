using Aosta.Core.Database.Enums;
using Aosta.Core.Database.Mapper;
using Aosta.Core.Database.Models.Embedded;
using Aosta.Core.Database.Models.Jikan;
using Aosta.Core.Database.Models.Local;

using Realms;

namespace Aosta.Core.Database.Models;


/// Anime model class
[Preserve(AllMembers = true)]
public partial class Anime : IRealmObject, IHasPrimaryKey<Guid>
{
    /// The user this anime belongs to
    public User? User { get; set; }

    /// The unique ID of this content
    [PrimaryKey]
    public Guid ID { get; private set; } = Guid.NewGuid();

    /// The online MyAnimeList metadata of this content
    public JikanAnime? Jikan { get; set; }

    /// The local metadata of this content (overrides online metadata)
    public LocalAnime? Local { get; set; }

    /// The episodes of this content
    public IList<Episode> Episodes { get; }

    public string? Title => Local?.Title ?? Jikan?.Titles.FirstOrDefault(entry => entry.Type == "Default", TitleEntry.EMPTY).Title;

    public string? Synopsis => Local?.Synopsis ?? Jikan?.Synopsis;

    public ContentType? Type => Local?.Type ?? Jikan?.Type;

    public string? Source => Local?.Source ?? Jikan?.Source;

    public AiringStatus? AiringStatus => Local?.AiringStatus ?? Jikan?.Status;

    public Season? Season => Local?.Season ?? Jikan?.Season;

    public int? Year => Local?.Year ?? Jikan?.Year;

    /// The score from 0 to 100
    [Indexed]
    public int? Score { get; set; }

    /// The review of this content
    public string Review { get; set; } = string.Empty;

    [Indexed]
    internal int _WatchStatus { get; private set; } = -1;

    ///  The watch status (e. g. "Completed")
    [Ignored]
    public WatchingStatus WatchingStatus
    {
        get => (WatchingStatus)_WatchStatus;
        set => _WatchStatus = (int)value;
    }
}
