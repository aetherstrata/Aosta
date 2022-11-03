using System.ComponentModel;
using System.Xml.Serialization;

namespace Animeikan.GUI.Data.Mal;

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
internal sealed class MalUserInfo
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
