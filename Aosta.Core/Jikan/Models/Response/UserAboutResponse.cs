﻿using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Extra information about user
/// </summary>
public class UserAboutResponse
{
	/// <summary>
	/// User self description
	/// </summary>
	[JsonPropertyName("about")]
	public string? About { get; set; }
}