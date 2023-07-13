using System.Net.Http.Headers;
using Aosta.Common;
using Aosta.Common.Limiter;
using Serilog;

namespace Aosta.Jikan;

public class JikanConfiguration
{
    internal const string DefaultEndpoint = "https://api.jikan.moe/v4/";

    private IEnumerable<TaskLimiterConfiguration>? _limiterConfigs;
    private HttpClient? _httpClient;
    private ILogger? _logger;

    public JikanServicesBuilder Use => new(this);

    public JikanFinalBuilder With => new(this);

    public IJikan Build()
    {
        _httpClient ??= GetDefaultHttpClient(DefaultEndpoint);
        ITaskLimiter limiter = new CompositeTaskLimiter(_limiterConfigs?.Distinct() ?? TaskLimiterConfiguration.Default);

        return new JikanClient(_httpClient, limiter, _logger);
    }

    /// <summary>
    /// Get static HttpClient with default parameters.
    /// </summary>
    /// <param name="endpoint">Endpoint of the REST API.</param>
    /// <returns>Static HttpClient.</returns>
    private static HttpClient GetDefaultHttpClient(string endpoint)
    {
        Guard.IsNotNullOrWhiteSpace(endpoint, nameof(endpoint));

        var client = new HttpClient { BaseAddress = new Uri(endpoint) };
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

        return client;
    }

    public class JikanServicesBuilder
    {
        private readonly JikanConfiguration _jikan;

        internal JikanServicesBuilder(JikanConfiguration jikan)
        {
            _jikan = jikan;
        }

        public JikanConfiguration Logger(ILogger logger)
        {
            _jikan._logger = logger;
            return _jikan;
        }

        public JikanConfiguration Logger(LoggerConfiguration logConfig)
        {
            _jikan._logger = logConfig.CreateLogger();
            return _jikan;
        }

        public JikanConfiguration Limiter(IEnumerable<TaskLimiterConfiguration> limiterConfigs)
        {
            _jikan._limiterConfigs = limiterConfigs;
            return _jikan;
        }
    }

    public class JikanFinalBuilder
    {
        private readonly JikanConfiguration _jikan;

        internal JikanFinalBuilder(JikanConfiguration jikan)
        {
            _jikan = jikan;
        }

        public IJikan Endpoint(string endpoint)
        {
            _jikan._httpClient = GetDefaultHttpClient(endpoint);
            return _jikan.Build();
        }

        public IJikan HttpClient(HttpClient httpClient)
        {
            _jikan._httpClient = httpClient;
            return _jikan.Build();
        }
    }
}