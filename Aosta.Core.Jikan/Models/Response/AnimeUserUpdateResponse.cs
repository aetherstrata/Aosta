using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Single anime user update model class.
/// </summary>
public class AnimeUserUpdateResponse : UserUpdateResponse
{
	/// <summary>
	/// Amount of episodes seen by the user.
	/// </summary>
	[JsonPropertyName("episodes_seen")]
	public int? EpisodesSeen { get; set; }

	/// <summary>
	/// Total amount of the episodes.
	/// </summary>
	[JsonPropertyName("episodes_total")]
	public int? EpisodesTotal { get; set; }
}