using System.Collections;
using Realms;

using System.Text.Json.Serialization;

namespace Animeikan.Core.Data;

public class ContentGroupDTO : RealmObject, IContent, IContentRating<ContentGroupDTO>, ICollection<EpisodeDTO>, IEquatable<ContentGroupDTO>
{
    /// <summary>The unique ID of this content group</summary>
    [PrimaryKey]
    [JsonPropertyName("id")]
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>The type of this content group</summary>
    [Indexed]
    [JsonPropertyName("contentType")]
    public int Type { get; set; }

    /// <summary>The title of this content group</summary>
    [Indexed]
    [JsonPropertyName("title")]
    public string Title { get; set; } = String.Empty;

    /// <summary>The description of this content group</summary>
    [JsonPropertyName("description")]
    public string Synopsis { get; set; } = String.Empty;

    /// <summary>The list of episodes of this content group</summary>
    [JsonPropertyName("episodes")]
    public IList<EpisodeDTO> Episodes { get; }

    /// <summary>The score of this content group</summary>
    [Indexed]
    [JsonPropertyName("score")]
    public int Score { get; set; } = -1;

    /// <summary>The review of this content group</summary>
    [JsonPropertyName("review")]
    public string Review { get; set; } = String.Empty;

    /// <summary>The episode count of this content group</summary>
    [JsonPropertyName("episodeCount")]
    public int Count { get => Episodes.Count; }

    [Ignored]
    public bool IsReadOnly { get => false; }

    public IEnumerator<EpisodeDTO> GetEnumerator() => Episodes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(EpisodeDTO item) => Episodes.Add(item);

    public void Clear() => Episodes.Clear();

    public bool Contains(EpisodeDTO item) => Episodes.Contains(item);

    public void CopyTo(EpisodeDTO[] array, int arrayIndex) => Episodes.CopyTo(array, arrayIndex);

    public bool Remove(EpisodeDTO item) => Episodes.Remove(item);

    public bool Equals(ContentGroupDTO? other)
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
        return Equals((ContentGroupDTO)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
