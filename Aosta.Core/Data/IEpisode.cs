using Aosta.Core.Data.Realm;

namespace Aosta.Core.Data;

public interface IEpisode
{
    /// <summary>The content this episode is from.</summary>
    public AnimeObject? Content { get; set; }

    /// <summary>The unique ID of this episode.</summary>
    public Guid Id { get; init; }

    /// <summary>The episode number.</summary>
    public int Number { get; set; }

    /// <summary>The episode title.</summary>
    public string? Title { get; set; }

    /// <summary>The episode description.</summary>
    public string Synopsis { get; set; }

    /// <summary>The episode score.</summary>
    public int Score { get; set; }

    /// <summary>The episode review.</summary>
    public string Review { get; set; }

    /// <summary>The duration of the episode in seconds.</summary>
    /// <remarks>
    /// It has to be stored as a pure number because <see cref="Realm"/> does not support <see cref="TimeSpan"/>.
    /// </remarks>
    public int? Duration { get; set; }

    /// <summary>Whether this episode has aired already.</summary>
    public bool HasAired { get; set; }

    /// <summary>The date when the episode first aired.</summary>
    public DateTimeOffset? Aired { get; set; }

    /// <summary>Whether this episode is filler.</summary>
    public bool IsFiller { get; set; }

    /// <summary>Whether this episode is a recap.</summary>
    public bool IsRecap { get; set; }
}