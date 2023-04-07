using System.Runtime.Serialization;

namespace Aosta.Jikan.Enums;

/// <summary>
/// Enumeration for anime types (search config).
/// </summary>
public enum ForumTopicType
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