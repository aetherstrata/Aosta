using System.Text.Json.Serialization;
using Aosta.Jikan.Enums;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Seasons archive model class
/// </summary>
public class SeasonArchiveResponse
{
	/// <summary>
	/// Available year to query.
	/// </summary>
	[JsonPropertyName("year")]
	public int Year { get; init; }

	/// <summary>
	/// Collection of available seasons in year to query.
	/// </summary>
	[JsonPropertyName("seasons")]
	public ICollection<Season>? Season { get; init; }
}