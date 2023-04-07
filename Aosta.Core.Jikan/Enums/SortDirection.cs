using System.Runtime.Serialization;

namespace Aosta.Core.Jikan.Enums;

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
	/// Use descending.
	/// </summary>
	[EnumMember(Value = "desc")]
	Descending,
}