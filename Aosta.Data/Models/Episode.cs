using Aosta.Data.Database;
using Aosta.Data.Models.Embedded;

using Realms;

namespace Aosta.Data.Models;


/// Episode model class

[Preserve(AllMembers = true)]
public partial class Episode : IEmbeddedObject
{
    /// ID associated with MyAnimeList.
    public long MalId { get; private set; }

    /// User's score on the episode.
    public double? Score { get; set; }

    /// Episode's score on MyAnimeList
    public double? OnlineScore { get; set; }

    /// URL to the episode.
    public string? Url { get; set; }

    /// Titles of the episode.
    public IList<TitleEntry> Titles { get; } = null!;

    /// Episode's duration.
    public int? Duration { get; set; }

    /// Date when episode aired at first.
    public DateTimeOffset? Aired { get; set; }

    /// Is the episode filler.
    public bool Filler { get; set; }

    /// Is the episode recap.
    public bool Recap { get; set; }

    /// Episode's synopsis.
    public string? Synopsis { get; set; }

    /// URL to forum discussion
    public string? ForumUrl{ get; set; }
}
