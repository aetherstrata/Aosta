namespace Aosta.Jikan.Query.Builder.Producer;

internal static class ProducerFullDataQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.PRODUCERS,
        id.ToString(),
        JikanEndpointConsts.FULL
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return JikanQuery.Create(getEndpoint(id));
    }
}
