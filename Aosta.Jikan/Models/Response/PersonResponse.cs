using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Person model class.
/// </summary>
public class PersonResponse
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; init; }

	/// <summary>
	/// Person's url.
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; init; }

	/// <summary>
	/// Person's name.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; init; }

	/// <summary>
	/// Person's given name..
	/// </summary>
	[JsonPropertyName("given_name")]
	public string? GivenName { get; init; }

	/// <summary>
	/// Person's family name.
	/// </summary>
	[JsonPropertyName("family_name")]
	public string? FamilyName { get; init; }

	/// <summary>
	/// Person's alternate names.
	/// </summary>
	[JsonPropertyName("alternate_names")]
	public ICollection<string>? AlternativeNames { get; init; }

	/// <summary>
	/// Person's birthday.
	/// </summary>
	[JsonPropertyName("birthday")]
	public DateTimeOffset? Birthday { get; init; }

	/// <summary>
	/// Person's website URL.
	/// </summary>
	[JsonPropertyName("website_url")]
	public string? WebsiteUrl { get; init; }

	/// <summary>
	/// Person's favourite count on MyAnimeList.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int? MemberFavorites { get; init; }

	/// <summary>
	/// More information about person.
	/// </summary>
	[JsonPropertyName("about")]
	public string? About { get; init; }

	/// <summary>
	/// Person's image set
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSetResponse? Images { get; init; }
}