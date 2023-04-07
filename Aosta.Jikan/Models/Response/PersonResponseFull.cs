using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Person with full data model class.
/// </summary>
public class PersonResponseFull: PersonResponse
{
    /// <summary>
    /// Animeography of person.
    /// </summary>
    [JsonPropertyName("anime")]
    public ICollection<PersonAnimeographyEntryResponse>? Animeography { get; set; }
    
    /// <summary>
    /// Mangaography of person.
    /// </summary>
    [JsonPropertyName("manga")]
    public ICollection<PersonMangaographyEntryResponse>? Mangaography { get; set; }
    
    /// <summary>
    /// Voice actors of person.
    /// </summary>
    [JsonPropertyName("voices")]
    public ICollection<VoiceActingRoleResponse>? VoiceActingRoles { get; set; }
}