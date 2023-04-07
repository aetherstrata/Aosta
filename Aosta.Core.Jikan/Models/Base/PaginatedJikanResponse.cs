using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Base;

/// <summary>
/// Base wrapping class for response with paginated data
/// </summary>
public class PaginatedJikanResponse<TResponse> : BaseJikanResponse<TResponse>
{
	/// <summary>
	/// Pagination
	/// </summary>
	[JsonPropertyName("pagination")]
	public required Pagination Pagination { get; set; }

	/// <summary>
	/// Parameterless constructor, required for serialization
	/// </summary>
	public PaginatedJikanResponse() { }
}