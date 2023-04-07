using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Manga characters list model class.
/// </summary>
public class MangaCharacterResponse
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
}