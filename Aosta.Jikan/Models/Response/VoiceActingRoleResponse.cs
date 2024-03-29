﻿using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Voice acting role model class for person's class.
/// </summary>
public class VoiceActingRoleResponse
{
	/// <summary>
	/// Anime associated with voice acting role.
	/// </summary>
	[JsonPropertyName("anime")]
	public MalImageSubItemResponse? Anime { get; init; }

	/// <summary>
	/// Character associated with voice acting role.
	/// </summary>
	[JsonPropertyName("character")]
	public MalImageSubItemResponse? Character { get; init; }

	/// <summary>
	/// Status of the role: Main/Supporting.
	/// </summary>
	[JsonPropertyName("role")]
	public string? Role { get; init; }
}