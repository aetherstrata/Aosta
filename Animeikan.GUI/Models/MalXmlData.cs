using System.ComponentModel;
using System.Xml.Serialization;

namespace Animeikan.GUI.Models
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "myanimelist", IsNullable = false)]
    internal sealed class MalXmlData
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
        public uint user_id { get; set; }
        [XmlElement("user_name")]
        public string user_name { get; set; }
        [XmlElement("user_export_type")]
        public byte user_export_type { get; set; }
        [XmlElement("user_total_anime")]
        public byte user_total_anime { get; set; }
        [XmlElement("user_total_watching")]
        public byte user_total_watching { get; set; }
        [XmlElement("user_total_completed")]
        public byte user_total_completed { get; set; }
        [XmlElement("user_total_onhold")]
        public byte user_total_onhold { get; set; }
        [XmlElement("user_total_dropped")]
        public byte user_total_dropped { get; set; }
        [XmlElement("user_total_plantowatch")]
        public byte user_total_plantowatch { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    internal sealed class MyAnimeListAnime
    {
        [XmlElement("series_animedb_id")]
        public ushort series_animedb_id { get; set; }
        [XmlElement("series_title")]
        public string series_title { get; set; }
        [XmlElement("series_type")]
        public string series_type { get; set; }
        [XmlElement("series_episodes")]
        public byte series_episodes { get; set; }
        [XmlElement("my_id")]
        public byte my_id { get; set; }
        [XmlElement("my_watched_episodes")]
        public byte my_watched_episodes { get; set; }
        [XmlElement("my_start_date")]
        public string my_start_date { get; set; }
        [XmlElement("my_finish_date")]
        public string my_finish_date { get; set; }
        [XmlElement("my_rated")]
        public object my_rated { get; set; }
        [XmlElement("my_score")]
        public byte my_score { get; set; }
        [XmlElement("my_storage")]
        public object my_storage { get; set; }
        [XmlElement("my_storage_value")]
        public decimal my_storage_value { get; set; }
        [XmlElement("my_status")]
        public string my_status { get; set; }
        [XmlElement("my_comments")]
        public string my_comments { get; set; }
        [XmlElement("my_times_watched")]
        public byte my_times_watched { get; set; }
        [XmlElement("my_rewatch_value")]
        public string my_rewatch_value { get; set; }
        [XmlElement("my_priority")]
        public string my_priority { get; set; }
        [XmlElement("my_tags")]
        public string my_tags { get; set; }
        [XmlElement("my_rewatching")]
        public byte my_rewatching { get; set; }
        [XmlElement("my_rewatching_ep")]
        public byte my_rewatching_ep { get; set; }
        [XmlElement("my_discuss")]
        public byte my_discuss { get; set; }
        [XmlElement("my_sns")]
        public string my_sns { get; set; }
        [XmlElement("update_on_import")]
        public byte update_on_import { get; set; }
    }
}