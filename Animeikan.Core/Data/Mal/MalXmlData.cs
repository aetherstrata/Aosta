using System.ComponentModel;
using System.Xml.Serialization;

namespace Animeikan.GUI.Data.Mal;

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "myanimelist", IsNullable = false)]
internal sealed class MalXmlData
{
  [XmlElement("myinfo")]
  public MalUserInfo MyInfo { get; set; }
  [XmlElement("anime")]
  public MalAnime[] Anime { get; set; }
}
