using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model of entry in recent/popular episodes list.
/// </summary>
public class WatchPromoVideoResponse : PromoVideoResponse
{
    /// <summary>
    /// Related anime entry
    /// </summary>
    [JsonPropertyName("entry")]
    public MalImageSubItemResponse? Entry { get; set; }
}