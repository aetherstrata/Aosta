using System.ComponentModel;
using System.Xml.Serialization;

namespace Aosta.Core.Data.Models.Xml;

/// <summary> MyAnimeList XML profile data model </summary>
[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "myanimelist", IsNullable = false)]
internal sealed class MalXmlData
{
  /// <summary> User info and statistics </summary>
  [XmlElement("myinfo")] public required MalUserInfo MyInfo { get; set; }

  /// <summary> User anime list </summary>
  [XmlElement("anime")] public required MalAnime[] Anime { get; set; }
}
