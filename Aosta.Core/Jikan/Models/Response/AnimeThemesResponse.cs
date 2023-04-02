using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Model representing anime openings and endings
/// </summary>
public class AnimeThemesResponse
{
	/// <summary>
	/// Anime openings.
	/// </summary>
	[JsonPropertyName("openings")]
	public ICollection<string>? Openings { get; set; }

	/// <summary>
	/// Anime endings.
	/// </summary>
	[JsonPropertyName("endings")]
	public ICollection<string>? Endings { get; set; }
}