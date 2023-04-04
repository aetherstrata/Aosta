﻿using System.Net;
using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models;

/// <summary>
/// Class of error data returned in case http call was unsuccessful
/// </summary>
public class JikanApiError
{
	/// <summary>
	/// Response code received from HttpResponseMessage.
	/// </summary>
	[JsonPropertyName("status")]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public HttpStatusCode Status { get; set; }

	/// <summary>
	/// Type of http error.
	/// </summary>
	[JsonPropertyName("type")]
	public string? Type { get; set; }

	/// <summary>
	/// Message of the error.
	/// </summary>
	[JsonPropertyName("message")]
	public string? Message { get; set; }

	/// <summary>
	/// Additional data.
	/// </summary>
	[JsonPropertyName("error")]
	public string? Error { get; set; }
}