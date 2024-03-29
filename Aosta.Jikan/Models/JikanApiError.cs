﻿using System.Net;
using System.Text.Json.Serialization;

namespace Aosta.Jikan.Models;

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
    public HttpStatusCode Status { get; init; }

    /// <summary>
    /// Type of http error.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Message of the error.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    /// <summary>
    /// Additional data.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }

    /// <inheritdoc />
    public override string ToString() =>
        $$"""
          {
              "{{nameof(Status)}}": "{{Status}}",
              "{{nameof(Type)}}": "{{Type}}",
              "{{nameof(Message)}}": "{{Message}}",
              "{{nameof(Error)}}": "{{Error}}"
          }
          """;
}
