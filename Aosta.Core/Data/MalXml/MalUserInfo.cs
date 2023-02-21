using System.ComponentModel;
using System.Xml.Serialization;

namespace Aosta.Core.Data.MalXml;

//TODO: add xmldoc

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
internal sealed class MalUserInfo
{
  [XmlElement("user_id")]
  public required uint UserId { get; set; }

  [XmlElement("user_name")]
  public required string UserName { get; set; }

  [XmlElement("user_export_type")]
  public byte UserExportType { get; set; } = 1;

  [XmlElement("user_total_anime")]
  public int UserTotalAnime { get; set; }

  [XmlElement("user_total_watching")]
  public int UserTotalWatching { get; set; }

  [XmlElement("user_total_completed")]
  public int UserTotalCompleted { get; set; }

  [XmlElement("user_total_onhold")]
  public int UserTotalOnHold { get; set; }

  [XmlElement("user_total_dropped")]
  public int UserTotalDropped { get; set; }

  [XmlElement("user_total_plantowatch")]
  public int UserTotalPlanToWatch { get; set; }
}
