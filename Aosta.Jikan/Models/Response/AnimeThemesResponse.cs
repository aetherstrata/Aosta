using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model representing anime openings and endings
/// </summary>
public class AnimeThemesResponse
{
	/// <summary>
	/// Anime openings.
	/// </summary>
	[JsonPropertyName("openings")]
	public ICollection<string>? Openings { get; init; }

	/// <summary>
	/// Anime endings.
	/// </summary>
	[JsonPropertyName("endings")]
	public ICollection<string>? Endings { get; init; }
}