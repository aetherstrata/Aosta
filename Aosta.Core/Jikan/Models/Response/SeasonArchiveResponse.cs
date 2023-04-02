using System.Text.Json.Serialization;
using Aosta.Core.Jikan.Enums;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Seasons archive model class
/// </summary>
public class SeasonArchiveResponse
{
	/// <summary>
	/// Available year to query.
	/// </summary>
	[JsonPropertyName("year")]
	public int Year { get; set; }

	/// <summary>
	/// Collection of available seasons in year to query.
	/// </summary>
	[JsonPropertyName("seasons")]
	public ICollection<Season>? Season { get; set; }
}