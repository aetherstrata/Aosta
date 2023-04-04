using Aosta.Core.Limiter;

namespace Aosta.Core.Jikan;

public class JikanConfiguration
{
    private const string DefaultEndpoint = "https://api.jikan.moe/v4/";

    public string Endpoint { get; init; } = DefaultEndpoint;

    public List<TaskLimiterConfiguration> LimiterConfigurations { get; init; } = TaskLimiterConfiguration.Default;
}