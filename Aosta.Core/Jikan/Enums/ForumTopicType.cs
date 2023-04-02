﻿using System.ComponentModel;

namespace Aosta.Core.Jikan.Enums;

/// <summary>
/// Enumeration for anime types (search config).
/// </summary>
public enum ForumTopicType
{
	/// <summary>
	/// All types.
	/// </summary>
	[Description("all")]
	All,

	/// <summary>
	/// Episode type.
	/// </summary>
	[Description("episode")]
	Episode,

	/// <summary>
	/// Other type.
	/// </summary>
	[Description("other")]
	Other
}