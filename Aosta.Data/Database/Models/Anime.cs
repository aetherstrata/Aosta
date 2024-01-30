using Aosta.Data.Extensions;
using Aosta.Data.Database.Enums;
using Aosta.Data.Database.Models.Jikan;
using Aosta.Data.Database.Models.Local;

using Realms;

namespace Aosta.Data.Database.Models;


/// Anime model class
[Preserve(AllMembers = true)]
public partial class Anime : IRealmObject, IHasPrimaryKey<Guid>
{
    /// The unique ID of this content
    [PrimaryKey]
    public Guid ID { get; private set; } = Guid.NewGuid();

    /// The online MyAnimeList metadata of this content
    public JikanAnime? Jikan { get; set; }

    /// The local metadata of this content (overrides online metadata)
    public LocalAnime? Local { get; set; }

    /// The episodes of this content
    public IList<Episode> Episodes { get; } = null!;

    public string? DefaultTitle => Local?.Title ?? Jikan?.Titles.GetDefault();

    public string? Synopsis => Local?.Synopsis ?? Jikan?.Synopsis;

    public ContentType? Type => Local?.Type ?? Jikan?.Type;

    public string? Source => Local?.Source ?? Jikan?.Source;

    public AiringStatus? AiringStatus => Local?.AiringStatus ?? Jikan?.Status;

    public Season? Season => Local?.Season ?? Jikan?.Season;

    public int? Year => Local?.Year ?? Jikan?.Year;

    /// The score from 0 to 100
    [Indexed]
    public int? UserScore { get; set; }

    /// The review of this content
    public string Review { get; set; } = string.Empty;

    [Indexed]
    internal int WatchStatus { get; private set; } = -1;

    ///  The watch status (e. g. "Completed")
    [Ignored]
    public WatchingStatus WatchingStatus
    {
        get => (WatchingStatus)WatchStatus;
        set => WatchStatus = (int)value;
    }

    public static readonly AnimeTitleComparator TITLE_COMPARATOR = new();

    public sealed class AnimeTitleComparator : IComparer<Anime>
    {
        public int Compare(Anime? x, Anime? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;

            int titleCompare = string.Compare(x.DefaultTitle, y.DefaultTitle, StringComparison.Ordinal);

            return titleCompare != 0 ? titleCompare : x.ID.CompareTo(y.ID);
        }
    }
}
