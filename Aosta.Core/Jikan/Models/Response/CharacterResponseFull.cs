using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Character with full data model class.
/// </summary>
public class CharacterResponseFull: CharacterResponse
{
    /// <summary>
    /// Animeography of character.
    /// </summary>
    [JsonPropertyName("anime")]
    public ICollection<CharacterAnimeographyEntryResponse>? Animeography { get; set; }
    
    /// <summary>
    /// Mangaography of character.
    /// </summary>
    [JsonPropertyName("manga")]
    public ICollection<CharacterMangaographyEntryResponse>? Mangaography { get; set; }
    
    /// <summary>
    /// Voice actors of character.
    /// </summary>
    [JsonPropertyName("voices")]
    public ICollection<VoiceActorEntryResponse>? VoiceActors { get; set; }
}