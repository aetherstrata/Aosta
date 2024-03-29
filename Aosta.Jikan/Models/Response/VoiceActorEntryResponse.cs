﻿using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model class representing voice actor entry on Character's page.
/// </summary>
public class VoiceActorEntryResponse
{
	/// <summary>
	/// Voice actor's language.
	/// </summary>
	[JsonPropertyName("language")]
	public string? Language { get; init; }

	/// <summary>
	/// Voice actor's details.
	/// </summary>
	[JsonPropertyName("person")]
	public MalImageSubItemResponse? Person { get; init; }
}