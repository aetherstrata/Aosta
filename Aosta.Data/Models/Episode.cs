using Aosta.Data.Database;
using Aosta.Data.Models.Embedded;

using Realms;

namespace Aosta.Data.Models;


/// Episode model class

[Preserve(AllMembers = true)]
public partial class Episode : IEmbeddedObject
{
    /// Episode number
    public int Number { get; set; }

    /// Notes on this episodes
    public IList<EpisodeNote> Notes { get; } = null!;

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

    /// Date when episode was watched
    public DateTimeOffset? Watched { get; set; }

    /// Is the episode filler.
    public bool Filler { get; set; }

    /// Is the episode recap.
    public bool Recap { get; set; }

    /// Episode's synopsis.
    public string? Synopsis { get; set; }

    /// URL to forum discussion
    public string? ForumUrl { get; set; }
}
