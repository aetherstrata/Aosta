using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// User with full data model class.
/// </summary>
public class UserResponseFull: UserProfileResponse
{
    /// <summary>
    /// User's anime and manga statistics
    /// </summary>
    [JsonPropertyName("statistics")]
    public UserStatisticsResponse? Statistics { get; set; }
    
    /// <summary>
    /// User's favorites
    /// </summary>
    [JsonPropertyName("favorites")]
    public UserFavoritesResponse? Favorites { get; set; }
    
    /// <summary>
    /// User's anime and manga updates
    /// </summary>
    [JsonPropertyName("updates")]
    public UserUpdatesResponse? Updates { get; set; }
    
    /// <summary>
    /// User's about
    /// </summary>
    [JsonPropertyName("about")]
    public string? About { get; set; }
    
    /// <summary>
    /// User's external links
    /// </summary>
    [JsonPropertyName("external")]
    public ICollection<ExternalLinkResponse>? External { get; set; }
}