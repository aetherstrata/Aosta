using System.ComponentModel;
using System.Xml.Serialization;

namespace Aosta.Core.Data.Models.Xml;

/// <summary> MyAnimeList XML user data model </summary>
[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
internal sealed class MalUserInfo
{
    /// <summary> MyAnimeList user ID </summary>
    [XmlElement("user_id")] public required uint UserId { get; set; }

    /// <summary> MyAnimeList user name </summary>
    [XmlElement("user_name")] public required string UserName { get; set; }

    /// <summary> MyAnimeList user export type </summary>
    [XmlElement("user_export_type")] public byte UserExportType { get; set; } = 1;

    /// <summary> MyAnimeList user total anime </summary>
    [XmlElement("user_total_anime")] public int UserTotalAnime { get; set; }

    /// <summary> MyAnimeList user total watching anime </summary>
    [XmlElement("user_total_watching")] public int UserTotalWatching { get; set; }

    /// <summary> MyAnimeList user total completed anime </summary>
    [XmlElement("user_total_completed")] public int UserTotalCompleted { get; set; }

    /// <summary> MyAnimeList user ID total on-hold anime </summary>
    [XmlElement("user_total_onhold")] public int UserTotalOnHold { get; set; }

    /// <summary> MyAnimeList user total dropped anime </summary>
    [XmlElement("user_total_dropped")] public int UserTotalDropped { get; set; }

    /// <summary> MyAnimeList user total plan-to-watch anime </summary>
    [XmlElement("user_total_plantowatch")] public int UserTotalPlanToWatch { get; set; }
}