using System.Text.Json.Serialization;
using Realms;

namespace Aosta.Core.Data;

/// <summary> MyAnimeList data model </summary>
public class MalDataObject : RealmObject
{
    /// <summary>The MAL ID.</summary>
    [PrimaryKey]
    [JsonPropertyName("mal_id")]
    public required int MalId { get; init; }

    /// <summary>The canonical link.</summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = String.Empty;

    /// <summary>The MAL score</summary>
    [Indexed]
    [JsonPropertyName("score")]
    public int Score { get; set; }

    /// <summary>Number of people that scored.</summary>
    [JsonPropertyName("scored_by")]
    public int? ScoredBy { get; set; }

    /// <summary>The score rank on MyAnimeList.</summary>
    [JsonPropertyName("rank")]
    public int? Rank { get; set; }

    /// <summary>The popularity rank on MyAnimeList.</summary>
    [JsonPropertyName("popularity")]
    public int? Popularity { get; set; }

    /// <summary>The member count on MyAnimeList.</summary>
    [JsonPropertyName("members")]
    public int? Members { get; set; }

    /// <summary>The favourite count on MyAnimeList.</summary>
    [JsonPropertyName("favorites")]
    public int? Favorites { get; set; }
}