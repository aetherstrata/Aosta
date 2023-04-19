using System.Text.Json.Serialization;
using Aosta.Jikan.Enums;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Broadcast Details
/// </summary>
public class AnimeBroadcastResponse
{
	/// <summary>Day of the week</summary>
	[JsonPropertyName("day")]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public DaysOfWeek? Day { get; init; }

	/// <summary>Time in 24 hour format</summary>
	[JsonPropertyName("time")]
	public string? Time { get; init; }

	/// <summary>Timezone (Tz Database format https://en.wikipedia.org/wiki/List_of_tz_database_time_zones)</summary>
	[JsonPropertyName("timezone")]
	public string? Timezone { get; init; }

	/// <summary>Raw parsed broadcast string</summary>
	[JsonPropertyName("string")]
	public string? String { get; init; }
}