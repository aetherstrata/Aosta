using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// News model class.
/// </summary>
public class NewsResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; init; }

	/// <summary>
	/// News' URL.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Title of the news.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; init; }

	/// <summary>
	/// News' publication date.
	/// </summary>
	[JsonPropertyName("date")]
	public DateTimeOffset? Date { get; init; }

	/// <summary>
	/// News' author username.
	/// </summary>
	[JsonPropertyName("author_username")]
	public string? Author { get; init; }

	/// <summary>
	/// News' author url.
	/// </summary>
	[JsonPropertyName("author_url")]
	public string? AuthorUrl { get; init; }

	/// <summary>
	/// News' forum URL.
	/// </summary>
	[JsonPropertyName("forum_url")]
	public string? ForumUrl { get; init; }

	/// <summary>
	/// News' images set.
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images{ get; init; }

	/// <summary>
	/// Amount of comments under news.
	/// </summary>
	[JsonPropertyName("comments")]
	public int? Comments { get; init; }

	/// <summary>
	/// News' excerptL.
	/// </summary>
	[JsonPropertyName("excerpt")]
	public string? Excerpt { get; init; }
}