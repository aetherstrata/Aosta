using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Model class for anime character entry.
/// </summary>
public class AnimeCharacterResponse
{
	/// <summary>
	/// Character details
	/// </summary>
	[JsonPropertyName("character")]
	public CharacterEntryResponse? Character { get; set; }

	/// <summary>
	/// Character's role (e. g. "main", "supporting")
	/// </summary>
	[JsonPropertyName("role")]
	public string? Role { get; set; }

	/// <summary>
	/// Character favourite count on MyAnimeList.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int? Favorites { get; set; }

	/// <summary>
	/// Character's list of voice actor in this entry (anime only).
	/// </summary>
	[JsonPropertyName("voice_actors")]
	public ICollection<VoiceActorEntryResponse>? VoiceActors { get; set; }
}