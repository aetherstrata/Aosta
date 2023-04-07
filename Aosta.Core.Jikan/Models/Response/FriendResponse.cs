﻿using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Model class for entry on user's history.
/// </summary>
public class FriendResponse
{
	/// <summary>
	/// Friend's user metadata.
	/// </summary>
	[JsonPropertyName("user")]
	public UserMetadataResponse? User { get; set; }

	/// <summary>
	/// Timestamp of friend's last online activity.
	/// </summary>
	[JsonPropertyName("last_online")]
	public DateTime? LastOnline { get; set; }

	/// <summary>
	/// Timestamp of friend addidition.
	/// </summary>
	[JsonPropertyName("friends_since")]
	public DateTime? FriendsSince { get; set; }
}