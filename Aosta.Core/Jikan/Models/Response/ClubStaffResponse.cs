﻿using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Club staffmodel class.
/// </summary>
public class ClubStaffResponse
{
	/// <summary>
	/// Club staff's Username.
	/// </summary>
	[JsonPropertyName("username")]
	public string? Username { get; set; }

	/// <summary>
	/// Club staff's URL
	/// </summary>
	[JsonPropertyName("url")]
	public string? Url { get; set; }
}