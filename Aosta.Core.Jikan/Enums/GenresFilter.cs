using System.Runtime.Serialization;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Enumeration for filtering genres
/// </summary>
public enum GenresFilter
{
	/// <summary>
	/// Genres.
	/// </summary>
	[EnumMember(Value = "genres")]
	Genres,

	/// <summary>
	/// Explicit genres.
	/// </summary>
	[EnumMember(Value = "explicit_genres")]
	ExplicitGenres,

	/// <summary>
	/// Themes.
	/// </summary>
	[EnumMember(Value = "themes")]
	Themes,

	/// <summary>
	/// Themes.
	/// </summary>
	[EnumMember(Value = "demographics")]
	Demographics
}