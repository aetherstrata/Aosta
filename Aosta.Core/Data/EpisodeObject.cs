using System.Text.Json.Serialization;
using Realms;

namespace Aosta.Core.Data;

/// <summary>Episode data-transfer-object for <see cref="Realm"/> storage.</summary>
public class EpisodeObject : RealmObject, IEpisode, IUserRating
{
    /// <summary>The content this episode is from.</summary>
    public AnimeObject? Content { get; set; }

    /// <summary>The unique ID of this episode.</summary>
    [PrimaryKey]
    [JsonPropertyName("episodeId")]
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>The episode number.</summary>
    [JsonPropertyName("episodeNumber")]
    public int Number { get; set; }

    /// <summary>The episode title.</summary>
    [JsonPropertyName("episodeTitle")]
    public string? Title { get; set; }

    /// <summary>The episode description.</summary>
    [JsonPropertyName("episodeSynopsis")]
    public string Synopsis { get; set; } = String.Empty;

    /// <summary>The episode score.</summary>
    [Indexed]
    [JsonPropertyName("episodeScore")]
    public int Score { get; set; }

    /// <summary>The episode review.</summary>
    [JsonPropertyName("episodeReview")]
    public string Review { get; set; } = String.Empty;

    /// <summary>The duration of the episode in seconds.</summary>
    /// <remarks>
    /// It has to be stored as a pure number because <see cref="Realm"/> does not support <see cref="TimeSpan"/>.
    /// </remarks>
    [JsonPropertyName("duration")]
    public int? Duration { get; set; }

    /// <summary>Whether this episode has aired already.</summary>
    [JsonPropertyName("hasAired")]
    public bool HasAired { get; set; }

    /// <summary>The date when the episode first aired.</summary>
    [JsonPropertyName("aired")]
    public DateTimeOffset? Aired { get; set; }

    /// <summary>Whether this episode is filler.</summary>
    [JsonPropertyName("isFiller")]
    public bool IsFiller { get; set; }

    /// <summary>Whether this episode is a recap.</summary>
    [JsonPropertyName("isRecap")]
    public bool IsRecap { get; set; }

    public override string ToString()
    {
        return $$"""
                Content: {{Content?.Title ?? "N/A"}}
                ID: {{Id}}
                Title: {{Title}}
                Synopsis: {{Synopsis}}
                Number: {{Number}}
                Score: {{Score}}
                Review: {{Review}}
                Duration: {{Duration}}
                HasAired: {{HasAired}}
                AiredDate: {{Aired}}
                IsFiller: {{IsFiller}}
                IsRecap: {{IsRecap}}
                """;
    }
}