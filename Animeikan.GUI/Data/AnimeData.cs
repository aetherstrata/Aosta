using Realms;

using System.Text.Json.Serialization;

#nullable enable

namespace Animeikan.GUI.Data;

public class AnimeData : RealmObject
{
  [PrimaryKey]
  [JsonPropertyName("id")]
  public Guid Id { get; set; } = Guid.NewGuid();

  [Indexed]
  [JsonPropertyName("title")]
  public required string Title { get; set; }

  [Indexed]
  [JsonPropertyName("score")]
  public uint Score { get; set; }
}
