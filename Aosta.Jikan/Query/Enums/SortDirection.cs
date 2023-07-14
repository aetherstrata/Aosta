using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Defines sort direction in search requests
/// </summary>
public enum SortDirection
{
	/// <summary>
	/// Sort ascending.
	/// </summary>
	[EnumMember(Value = "asc")]
	Ascending,

	/// <summary>
	/// Sort descending.
	/// </summary>
	[EnumMember(Value = "desc")]
	Descending,
}