using System.Text.Json;
using Aosta.Common.Consts;
using Aosta.Common.Limiter;
using Aosta.Jikan.Models;
using Serilog;

namespace Aosta.Jikan.Query;

internal sealed class QueryExecutor : IDisposable
{
    internal string BaseAddress { get; }

    private readonly HttpClient _http;
    private readonly ITaskLimiter _limiter;
    private readonly ILogger? _logger;

    internal QueryExecutor(HttpClient http, ITaskLimiter limiter, ILogger? logger = null)
    {
        BaseAddress = http.BaseAddress!.ToString();

        _http = http;
        _limiter = limiter;
        _logger = logger;
    }

    internal async Task<T> GetRequestAsync<T>(IQuery<T> query, CancellationToken ct = default)
    {
        string queryEndpoint = query.GetQuery();
        string fullUrl = _http.BaseAddress + queryEndpoint;
        try
        {
            _logger?.Debug("Performing GET request: \"{Request}\"", fullUrl);
            using var response = await _limiter.LimitAsync(() => _http.GetAsync(queryEndpoint, ct));

            string json = await response.Content.ReadAsStringAsync(ct);
            _logger?.Verbose("Retrieved JSON string: {Json}", json);

            if (response.IsSuccessStatusCode)
            {
                _logger?.Debug("Got HTTP response for \"{Request}\" successfully", fullUrl);
                return JsonSerializer.Deserialize<T>(json) ?? throw new JikanRequestException(
                    ErrorMessages.SerializationNullResult + Environment.NewLine
                    + "Raw JSON string:" + Environment.NewLine + json);
            }

            _logger?.Error("Failed to get HTTP resource for \"{Resource}\", Status Code: {Status}", queryEndpoint, response.StatusCode);
            var errorData = JsonSerializer.Deserialize<JikanApiError>(json);
            throw new JikanRequestException(string.Format(ErrorMessages.FailedRequest, response.StatusCode, response.Content), errorData);
        }
        catch (JsonException ex)
        {
            throw new JikanRequestException(
                ErrorMessages.SerializationFailed + Environment.NewLine + "Inner exception message: " + ex.Message, ex);
        }
    }

    public void Dispose()
    {
        _http.Dispose();
    }
}