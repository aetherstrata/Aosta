using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Collection of user updates model class.
/// </summary>
public class UserUpdatesResponse
{
    /// <summary>
    /// Anime updates.
    /// </summary>
    [JsonPropertyName("anime")]
    public ICollection<AnimeUserUpdateResponse>? AnimeUpdates { get; set; }

    /// <summary>
    /// Manga updates.
    /// </summary>
    [JsonPropertyName("manga")]
    public ICollection<MangaUserUpdateResponse>? MangaUpdates { get; set; }
}