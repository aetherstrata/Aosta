using System.Runtime.Serialization;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Enumeration for club types (search config).
/// </summary>
public enum ClubType
{
	/// <summary>
	/// Allow all types to be displayed in results.
	/// </summary>
	[EnumMember(Value = "")]
	EveryType,

	/// <summary>
	/// Public clubs
	/// </summary>
	[EnumMember(Value = "public")]
	Public,

	/// <summary>
	/// Private clubs.
	/// </summary>
	[EnumMember(Value = "private")]
	Private,

	/// <summary>
	/// Secret clubs.
	/// </summary>
	[EnumMember(Value = "secret")]
	Secret
}