using System.Runtime.Serialization;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Enumeration for club categories (search config).
/// </summary>
public enum ClubCategory
{
	/// <summary>
	/// Allow all categories to be displayed in results.
	/// </summary>
	[EnumMember(Value = "")]
	EveryCategory,

	/// <summary>
	/// Anime category
	/// </summary>
	[EnumMember(Value = "anim")]
	Anime,

	/// <summary>
	/// Manga category
	/// </summary>
	[EnumMember(Value = "manga")]
	Manga,

	/// <summary>
	/// Actors and artists category
	/// </summary>
	[EnumMember(Value = "actors_and_artists")]
	ActorsAndArtists,

	/// <summary>
	/// Characters category
	/// </summary>
	[EnumMember(Value = "characters")]
	Characters,

	/// <summary>
	/// Cities and neighbourhoods category
	/// </summary>
	[EnumMember(Value = "cities_and_neighbourhoods")]
	CitiesAndNeighbourhoods,

	/// <summary>
	/// Companies category
	/// </summary>
	[EnumMember(Value = "companies")]
	Companies,

	/// <summary>
	/// Conventions category
	/// </summary>
	[EnumMember(Value = "conventions")]
	Conventions,

	/// <summary>
	/// Games category
	/// </summary>
	[EnumMember(Value = "games")]
	Games,

	/// <summary>
	/// Japan category
	/// </summary>
	[EnumMember(Value = "japan")]
	Japan,

	/// <summary>
	/// Music category
	/// </summary>
	[EnumMember(Value = "music")]
	Music,

	/// <summary>
	/// Schools category
	/// </summary>
	[EnumMember(Value = "shools")]
	Schools,

	/// <summary>
	/// Other category
	/// </summary>
	[EnumMember(Value = "other")]
	Other,
}