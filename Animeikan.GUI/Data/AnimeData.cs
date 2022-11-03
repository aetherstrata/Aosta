using Realms;

namespace Animeikan.GUI.Data;

public class AnimeData : RealmObject
{
  [PrimaryKey]
  public Guid Id { get; set; } = Guid.NewGuid();

  [Indexed]
  public required string Title { get; set; }

  public string Synopsis { get; set; }
}
