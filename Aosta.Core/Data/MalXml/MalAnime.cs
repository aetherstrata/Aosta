using System.ComponentModel;
using System.Xml.Serialization;

namespace Aosta.Core.Data.MalXml;

//TODO: add xml-doc

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
internal sealed class MalAnime
{
  [XmlElement("series_animedb_id")]
  public required ushort SeriesAnimeDbId { get; set; }

  [XmlElement("series_title")]
  public required string SeriesTitle { get; set; }

  [XmlElement("series_type")]
  public required string SeriesType { get; set; }

  [XmlElement("series_episodes")]
  public byte SeriesEpisodes { get; set; }

  [XmlElement("my_id")]
  public byte MyId { get; set; }

  [XmlElement("my_watched_episodes")]
  public byte MyWatchedEpisodes { get; set; }

  [XmlElement("my_start_date")]
  public string MyStartDate { get; set; } = "0000-00-00";

  [XmlElement("my_finish_date")]
  public string MyFinishDate { get; set; } = "0000-00-00";

  [XmlElement("my_rated")]
  public string MyRated { get; set; } = String.Empty;

  [XmlElement("my_score")]
  public byte MyScore { get; set; }

  [XmlElement("my_storage")]
  public string MyStorage { get; set; } = String.Empty;

  [XmlElement("my_storage_value")]
  public string MyStorageValue { get; set; } = "0.00";

  [XmlElement("my_status")]
  public string MyStatus { get; set; } = "Plan to Watch";

  [XmlElement("my_comments")]
  public string MyComments { get; set; } = "<![CDATA[]]>";

  [XmlElement("my_times_watched")]
  public byte MyTimesWatched { get; set; }

  [XmlElement("my_rewatch_value")]
  public string MyRewatchValue { get; set; } = String.Empty;

  [XmlElement("my_priority")]
  public string MyPriority { get; set; } = "LOW";

  [XmlElement("my_tags")]
  public string MyTags { get; set; } = "<![CDATA[]]>";

  [XmlElement("my_rewatching")]
  public byte MyRewatching { get; set; }

  [XmlElement("my_rewatching_ep")]
  public byte MyRewatchingEp { get; set; }

  [XmlElement("my_discuss")]
  public byte MyDiscuss { get; set; } = 1;

  [XmlElement("my_sns")]
  public string MySns { get; set; } = "default";

  /// <summary> Whether this entry should be updated when imported on MyAnimeList </summary>
  [XmlElement("update_on_import")]
  public byte UpdateOnImport { get; set; } = 0;
}
