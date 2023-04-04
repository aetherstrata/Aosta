using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Model of entry in recent/popular episodes list.
/// </summary>
public class WatchEpisodeResponse
{
    /// <summary>
    /// Is the episode region locked.
    /// </summary>
    [JsonPropertyName("region_locked")]
    public bool? RegionLocked { get; set; }
        
    /// <summary>
    /// Related anime entry
    /// </summary>
    [JsonPropertyName("entry")]
    public MalImageSubItemResponse? Entry { get; set; }
        
    /// <summary>
    /// List of available episodes
    /// </summary>
    [JsonPropertyName("episodes")]
    public ICollection<WatchEpisodeDetailsResponse>? Episodes { get; set; }
}