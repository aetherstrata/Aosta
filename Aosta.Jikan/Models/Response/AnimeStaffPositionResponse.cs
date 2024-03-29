﻿using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Model class for anime staff position (person request).
/// </summary>
public class AnimeStaffPositionResponse
{
	/// <summary>
	/// Person details.
	/// </summary>
	[JsonPropertyName("person")]
	public MalImageSubItemResponse? Person { get; init; }

	/// <summary>
	/// Positions associated with staff member.
	/// </summary>
	[JsonPropertyName("positions")]
	public ICollection<string>? Position { get; init; }
}