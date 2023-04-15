using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Character with full data model class.
/// </summary>
public class CharacterResponseFull : CharacterResponse
{
    /// <summary>
    /// Animeography of character.
    /// </summary>
    [JsonPropertyName("anime")]
    public ICollection<CharacterAnimeographyEntryResponse> Animeography { get; init; }

    /// <summary>
    /// Mangaography of character.
    /// </summary>
    [JsonPropertyName("manga")]
    public ICollection<CharacterMangaographyEntryResponse> Mangaography { get; init; }

    /// <summary>
    /// Voice actors of character.
    /// </summary>
    [JsonPropertyName("voices")]
    public ICollection<VoiceActorEntryResponse> VoiceActors { get; init; }
}