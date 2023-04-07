using System.Net.Http.Headers;
using Aosta.Core.Utils.Helpers;
using Aosta.Core.Utils.Limiter;
using Serilog;

namespace Aosta.Core.Jikan;

public class JikanConfiguration
{
    private const string DefaultEndpoint = "https://api.jikan.moe/v4/";

    private ITaskLimiter? _limiter;
    private HttpClient? _httpClient;
    private ILogger? _logger;

    public JikanServicesBuilder Use => new(this);

    public JikanFinalBuilder With => new(this);

    public IJikan Build()
    {
        _httpClient ??= GetDefaultHttpClient(DefaultEndpoint);
        _limiter ??= new CompositeTaskLimiter(TaskLimiterConfiguration.Default);

        return new JikanClient(_httpClient, _limiter, _logger);
    }

    /// <summary>
    /// Get static HttpClient with default parameters.
    /// </summary>
    /// <param name="endpoint">Endpoint of the REST API.</param>
    /// <returns>Static HttpClient.</returns>
    private static HttpClient GetDefaultHttpClient(string endpoint)
    {
        Guard.IsNotNullOrWhiteSpace(endpoint, nameof(endpoint));

        var client = new HttpClient() { BaseAddress = new Uri(endpoint) };
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

        public JikanConfiguration Limiter(ITaskLimiter taskLimiter)
        {
            _jikan._limiter = taskLimiter;
            return _jikan;
        }

        public JikanConfiguration Limiter(IEnumerable<TaskLimiterConfiguration> limiterConfigs)
        {
            _jikan._limiter = new CompositeTaskLimiter(limiterConfigs);
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