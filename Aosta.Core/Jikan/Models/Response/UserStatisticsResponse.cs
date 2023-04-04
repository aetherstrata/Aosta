using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Model representing user statistics
/// </summary>
public class UserStatisticsResponse
{
	/// <summary>
	/// User's anime statistics.
	/// </summary>
	[JsonPropertyName("anime")]
	public UserAnimeStatisticsResponse? AnimeStatistics { get; set; }

	/// <summary>
	/// User's manga statistics.
	/// </summary>
	[JsonPropertyName("manga")]
	public UserMangaStatisticsResponse? MangaStatistics { get; set; }
}