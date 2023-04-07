﻿using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models.Response;

/// <summary>
/// Single base user update model class.
/// </summary>
public class UserUpdateResponse
{
	/// <summary>
	/// Data about the user who made the update. Null if checked for specific user.
	/// </summary>
	[JsonPropertyName("user")]
	public UserMetadataResponse? User { get; set; }
		
	/// <summary>
	/// Data about the manga/anime the update. Null if checked for specific manga/anime.
	/// </summary>
	[JsonPropertyName("entry")]
	public MalImageSubItemResponse? Entry { get; set; }

	/// <summary>
	/// User's score.
	/// </summary>
	[JsonPropertyName("score")]
	public int? Score { get; set; }

	/// <summary>
	/// Date ofd the update.
	/// </summary>
	[JsonPropertyName("date")]
	public DateTimeOffset? Date { get; set; }

	/// <summary>
	/// Status (reading, watching, completed, etc.)
	/// </summary>
	[JsonPropertyName("status")]
	public string? Status { get; set; }
}