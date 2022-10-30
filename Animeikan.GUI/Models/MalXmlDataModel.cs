using System.ComponentModel;
using System.Xml.Serialization;

namespace Animeikan.GUI.Models
{
  [Serializable()]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true)]
  [XmlRoot(Namespace = "myanimelist", IsNullable = false)]
  internal sealed class MalXmlDataRoot
  {
    [XmlElement("myinfo")]
    public MyAnimeListMyinfo MyInfo { get; set; }
    [XmlElement("anime")]
    public MyAnimeListAnime[] Anime { get; set; }
  }

  [Serializable()]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true)]
  internal sealed class MyAnimeListMyinfo
  {
    [XmlElement("user_id")]
    public uint UserId { get; set; }
    [XmlElement("user_name")]
    public string UserName { get; set; }
    [XmlElement("user_export_type")]
    public byte UserExportType { get; set; }
    [XmlElement("user_total_anime")]
    public byte UserTotalAnime { get; set; }
    [XmlElement("user_total_watching")]
    public byte UserTotalWatching { get; set; }
    [XmlElement("user_total_completed")]
    public byte UserTotalCompleted { get; set; }
    [XmlElement("user_total_onhold")]
    public byte UserTotalOnHold { get; set; }
    [XmlElement("user_total_dropped")]
    public byte UserTotalDropped { get; set; }
    [XmlElement("user_total_plantowatch")]
    public byte UserTotalPlanToWatch { get; set; }
  }

  [Serializable()]
  [DesignerCategory("code")]
  [XmlType(AnonymousType = true)]
  internal sealed class MyAnimeListAnime
  {
    [XmlElement("series_animedb_id")]
    public ushort SeriesAnimeDbId { get; set; }
    [XmlElement("series_title")]
    public string SeriesTitle { get; set; }
    [XmlElement("series_type")]
    public string SeriesType { get; set; }
    [XmlElement("series_episodes")]
    public byte SeriesEpisodes { get; set; }
    [XmlElement("my_id")]
    public byte MyId { get; set; }
    [XmlElement("my_watched_episodes")]
    public byte MyWatchedEpisodes { get; set; }
    [XmlElement("my_start_date")]
    public string MyStartDate { get; set; }
    [XmlElement("my_finish_date")]
    public string MyFinishDate { get; set; }
    [XmlElement("my_rated")]
    public object MyRated { get; set; }
    [XmlElement("my_score")]
    public byte MyScore { get; set; }
    [XmlElement("my_storage")]
    public object MyStorage { get; set; }
    [XmlElement("my_storage_value")]
    public decimal MyStorageValue { get; set; }
    [XmlElement("my_status")]
    public string MyStatus { get; set; }
    [XmlElement("my_comments")]
    public string MyComments { get; set; }
    [XmlElement("my_times_watched")]
    public byte MyTimesWatched { get; set; }
    [XmlElement("my_rewatch_value")]
    public string MyRewatchValue { get; set; }
    [XmlElement("my_priority")]
    public string MyPriority { get; set; }
    [XmlElement("my_tags")]
    public string MyTags { get; set; }
    [XmlElement("my_rewatching")]
    public byte MyRewatching { get; set; }
    [XmlElement("my_rewatching_ep")]
    public byte MyRewatchingEp { get; set; }
    [XmlElement("my_discuss")]
    public byte MyDiscuss { get; set; }
    [XmlElement("my_sns")]
    public string MySns { get; set; }
    [XmlElement("update_on_import")]
    public byte UpdateOnImport { get; set; }
  }
}
