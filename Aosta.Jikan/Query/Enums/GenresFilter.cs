using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Enumeration for filtering genres
/// </summary>
public enum GenresFilter
{
	/// <summary>
	/// Return genres only.
	/// </summary>
	[EnumMember(Value = "genres")]
	Genres,

	/// <summary>
	/// Return explicit genres only.
	/// </summary>
	[EnumMember(Value = "explicit_genres")]
	ExplicitGenres,

	/// <summary>
	/// Return themes only.
	/// </summary>
	[EnumMember(Value = "themes")]
	Themes,

	/// <summary>
	/// Return demographics only.
	/// </summary>
	[EnumMember(Value = "demographics")]
	Demographics
}