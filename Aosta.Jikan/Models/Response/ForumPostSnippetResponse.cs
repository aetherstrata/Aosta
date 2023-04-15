using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model for basic information about forum post.
/// </summary>
public class ForumPostSnippetResponse
{
	/// <summary>
	/// Url to post.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Post's author username.
	/// </summary>
	[JsonPropertyName("author_username")]
	public string? AuthorUsername { get; init; }

	/// <summary>
	/// URL to profile of post author.
	/// </summary>
	[JsonPropertyName("author_url")]
	public string? AuthorUrl { get; init; }

	/// <summary>
	/// Date when the post was publicated.
	/// </summary>
	[JsonPropertyName("date")]
	public DateTimeOffset? Date { get; init; }
}