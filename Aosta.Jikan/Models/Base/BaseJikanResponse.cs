using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Base;

/// <summary>
/// Base wrapping class for response with data
/// </summary>
public class BaseJikanResponse<TResponse>
{
	/// <summary>
	/// Data of the request.
	/// </summary>
	[JsonPropertyName("data")]
	public required TResponse Data { get; set; }

	/// <summary>
	/// Parametereless constructor, required for serialization
	/// </summary>
	public BaseJikanResponse() {}
}