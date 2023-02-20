using Realms;

using System.Text.Json.Serialization;
using Aosta.Core.Data.Status;

namespace Aosta.Core.Data.Realm;

public class AnimeObject : RealmObject, IContent, IUserRating, IEquatable<AnimeObject>
{
    /// <summary>The unique ID of this content</summary>
    [PrimaryKey]
    [JsonPropertyName("id")]
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>The online MyAnimeList metadata of this content</summary>
    [JsonPropertyName("mal_data")]
    public MalDataObject? MalData { get; set; }

    [Indexed]
    [JsonPropertyName("content_type")]
    private int _Type { get; set; } = -1;

    /// <summary>The type of this content. (Eg. Movie, OVA)</summary>
    public ContentType Type
    {
        get => (ContentType)_Type;
        set => _Type = (int)value;
    }

    /// <summary>The title of this content</summary>
    [Indexed]
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>The description of this content</summary>
    [JsonPropertyName("description")]
    public string Synopsis { get; set; } = string.Empty;

    /// <summary>The episode count of this content</summary>
    [JsonPropertyName("episode_count")]
    public int? EpisodeCount { get; set; } = 1;

    /// <summary>The list of episodes of this content</summary>
    /// <remarks>This is an inverse relationship link to <see cref="EpisodeObject"/>.<see cref="EpisodeObject.Content"/></remarks>
    [Backlink(nameof(EpisodeObject.Content))]
    [JsonPropertyName("episodes")]
    public IQueryable<EpisodeObject>? Episodes { get; }

    /// <summary>Content source (e .g. "Manga" or "Original").</summary>
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;

    /// <summary>The score from 0 to 100</summary>
    [Indexed]
    [JsonPropertyName("score")]
    public int Score { get; set; } = -1;

    /// <summary>The review of this content</summary>
    [JsonPropertyName("review")]
    public string Review { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    private int _AiringStatus { get; set; } = 0;

    /// <summary>The airing status (e. g. "Currently Airing").</summary>
    public AiringStatus AiringStatus
    {
        get => (AiringStatus)_AiringStatus;
        set => _AiringStatus = (int)value;
    }

    /// <summary>Whether it is currently airing.</summary>
    [JsonPropertyName("airing")]
    public bool Airing { get; set; }

    /// <summary>The JPEG images of this content</summary>
    [JsonPropertyName("images_jpg")]
    public OnlineImageObject? ImageJPG { get; set; }

    /// <summary>The WebP images of this content</summary>
    [JsonPropertyName("images_webp")]
    public OnlineImageObject? ImageWebP { get; set; }

    public bool Equals(AnimeObject? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AnimeObject)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
