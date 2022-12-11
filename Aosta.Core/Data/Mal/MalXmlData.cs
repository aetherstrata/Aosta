using System.ComponentModel;
using System.Xml.Serialization;

namespace Aosta.Core.Data.Mal;

//TODO: add xmldoc

[Serializable()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "myanimelist", IsNullable = false)]
internal sealed class MalXmlData
{
  [XmlElement("myinfo")]
  public required MalUserInfo MyInfo { get; set; }

  [XmlElement("anime")]
  public required MalAnime[] Anime { get; set; }
}
