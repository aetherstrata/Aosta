namespace Aosta.Jikan.Query.Builder.Producer;

internal static class ProducerQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.PRODUCERS,
        id.ToString()
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return JikanQuery.Create(getEndpoint(id));
    }
}
