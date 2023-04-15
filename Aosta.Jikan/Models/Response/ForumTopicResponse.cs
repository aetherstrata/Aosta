using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model class for MyAnimeList forum topic.
/// </summary>
public class ForumTopicResponse
{
	/// <summary>
	/// Topic's MyAnimeList Id.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; init; }

	/// <summary>
	/// Topic's URL.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Topic's title.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>
	/// Date of topic start.
	/// </summary>
	[JsonPropertyName("date")]
	public DateTimeOffset? Date { get; init; }

	/// <summary>
	/// Topic's author username.
	/// </summary>
	[JsonPropertyName("author_username")]
	public string? AuthorUsername { get; init; }

	/// <summary>
	/// URL to profile of topic author.
	/// </summary>
	[JsonPropertyName("author_url")]
	public string? AuthorUrl { get; init; }

	/// <summary>
	/// Comment count.
	/// </summary>
	[JsonPropertyName("comments")]
	public int? Comments { get; init; }

	/// <summary>
	/// Basic information about last comment in the topic.
	/// </summary>
	[JsonPropertyName("last_comment")]
	public ForumPostSnippetResponse? LastPost { get; init; }
}