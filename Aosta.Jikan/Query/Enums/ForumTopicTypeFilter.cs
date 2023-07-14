using System.Runtime.Serialization;

namespace Aosta.Jikan.Query.Enums;

/// <summary>
/// Enumeration for anime types (search config).
/// </summary>
public enum ForumTopicTypeFilter
{
	/// <summary>
	/// All types.
	/// </summary>
	[EnumMember(Value = "all")]
	All,

	/// <summary>
	/// Episode type.
	/// </summary>
	[EnumMember(Value = "episode")]
	Episode,

	/// <summary>
	/// Other type.
	/// </summary>
	[EnumMember(Value = "other")]
	Other
}