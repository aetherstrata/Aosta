using System.Collections;
using Realms;

using System.Text.Json.Serialization;

namespace Aosta.Core.Data;

public class ContentDTO : RealmObject, IContent, IUserRating, IEquatable<ContentDTO>
{
    /// <summary>The unique ID of this content</summary>
    [PrimaryKey]
    [JsonPropertyName("id")]
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>The type of this content</summary>
    [Indexed]
    [JsonPropertyName("contentType")]
    public string? Type { get; set; }

    /// <summary>The title of this content</summary>
    [Indexed]
    [JsonPropertyName("title")]
    public string Title { get; set; } = String.Empty;

    /// <summary>The description of this content</summary>
    [JsonPropertyName("description")]
    public string Synopsis { get; set; } = String.Empty;

    /// <summary>The episode count of this content</summary>
    [JsonPropertyName("episodeCount")]
    public int? EpisodeCount { get; set; }

    /// <summary>The list of episodes of this content</summary>
    /// <remarks>This is an inverse relationship link to <see cref="EpisodeDTO"/>.<see cref="EpisodeDTO.Content"/></remarks>
    [Backlink(nameof(EpisodeDTO.Content))]
    [JsonPropertyName("episodes")]
    public IQueryable<EpisodeDTO>? Episodes { get; }

    /// <summary>Content source (e .g. "Manga" or "Original").</summary>
    [JsonPropertyName("source")]
    public string Source { get; set; }

    /// <summary>The score of this content</summary>
    [Indexed]
    [JsonPropertyName("score")]
    public int Score { get; set; } = -1;

    /// <summary>The review of this content</summary>
    [JsonPropertyName("review")]
    public string Review { get; set; } = String.Empty;

    public bool Equals(ContentDTO? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ContentDTO)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
