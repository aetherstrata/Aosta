namespace Aosta.Jikan.Query;

internal static class RandomPersonQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.People
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }
}