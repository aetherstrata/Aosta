using System.ComponentModel;
using System.Xml.Serialization;

namespace Aosta.Core.Data.Models.Xml;

/// <summary> MyAnimeList XML anime data model </summary>
[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
internal sealed class MalAnime
{
    /// <summary> Anime ID on MyAnimeList database </summary>
    [XmlElement("series_animedb_id")] public required ushort SeriesAnimeDbId { get; set; }

    /// <summary> Anime title on MyAnimeList database </summary>
    /// <remarks> Data is nested in a CDATA tag </remarks>
    [XmlElement("series_title")] public required string SeriesTitle { get; set; }

    /// <summary> Anime type on MyAnimeList database (eg. "TV", "OVA", "Movie")</summary>
    [XmlElement("series_type")] public required string SeriesType { get; set; }

    /// <summary> Anime episode count on MyAnimeList database </summary>
    [XmlElement("series_episodes")] public short SeriesEpisodes { get; set; }

    [XmlElement("my_id")] public byte MyId { get; set; }

    /// <summary> Number of episodes watched by the user </summary>
    [XmlElement("my_watched_episodes")] public byte MyWatchedEpisodes { get; set; }

    /// <summary> Date this anime was started by the user </summary>
    [XmlElement("my_start_date")] public string MyStartDate { get; set; } = "0000-00-00";

    /// <summary> Date this anime was finished by the user </summary>
    [XmlElement("my_finish_date")] public string MyFinishDate { get; set; } = "0000-00-00";

    /// <summary> User rating/review </summary>
    [XmlElement("my_rated")] public string MyRated { get; set; } = string.Empty;

    /// <summary> User score </summary>
    /// <remarks> Integer between 0 and 10 </remarks>
    [XmlElement("my_score")] public byte MyScore { get; set; }

    /// <summary> The device this anime was stored in by the user (eg. "Hard Drive", "NAS", "VHS") </summary>
    [XmlElement("my_storage")] public string MyStorage { get; set; } = string.Empty;

    /// <summary> Value of the user storage </summary>
    [XmlElement("my_storage_value")] public string MyStorageValue { get; set; } = "0.00";

    /// <summary> User watch status (eg, "Completed", "Watching") </summary>
    [XmlElement("my_status")] public string MyStatus { get; set; } = "Plan to Watch";

    /// <summary> User comment </summary>
    [XmlElement("my_comments")] public string MyComments { get; set; } = "<![CDATA[]]>";

    /// <summary> Times the user watched this anime </summary>
    [XmlElement("my_times_watched")] public byte MyTimesWatched { get; set; }

    /// <summary> User likelihood to rewatch this anime </summary>
    /// <remarks> Valid values are: <br/> <c>Very Low</c>, <c>Low</c>, <c>Medium</c>, <c>High</c>, <c>Very High</c> </remarks>
    [XmlElement("my_rewatch_value")] public string MyRewatchValue { get; set; } = string.Empty;

    /// <summary> User watch priority </summary>
    /// <remarks> Valid values are: <br/> <c>LOW</c>, <c>MEDIUM</c>, <c>HIGH</c> </remarks>
    [XmlElement("my_priority")] public string MyPriority { get; set; } = "LOW";

    /// <summary> User tags </summary>
    /// <remarks> Data is nested in a CDATA tag </remarks>
    [XmlElement("my_tags")] public string MyTags { get; set; } = "<![CDATA[]]>";

    /// <summary> Is the user rewatching this anime? </summary>
    [XmlElement("my_rewatching")] public byte MyRewatching { get; set; } = 0;

    /// <summary> Last episode the user watched during a rewatch </summary>
    [XmlElement("my_rewatching_ep")] public byte MyRewatchingEp { get; set; } = 0;

    /// <summary> Should MyAnimeList ask the user to discuss after watching an episode? </summary>
    /// <remarks>Valid values are: <br/> <c>0</c> - Do not ask <br/> <c>1</c> - Ask every time </remarks>
    [XmlElement("my_discuss")] public byte MyDiscuss { get; set; } = 1;

    /// <summary> Should MyAnimeList post to user SNS? </summary>
    /// <remarks> Valid values are: <br/> <c>default</c>, <c>ask_every_time</c>, <c>allow</c>, <c>disallow</c> </remarks>
    [XmlElement("my_sns")] public string MySns { get; set; } = "default";

    /// <summary> Whether this entry should be updated when imported on MyAnimeList </summary>
    [XmlElement("update_on_import")] public byte UpdateOnImport { get; set; }
}