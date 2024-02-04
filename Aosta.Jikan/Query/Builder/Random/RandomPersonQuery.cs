namespace Aosta.Jikan.Query.Builder.Random;

internal static class RandomPersonQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.RANDOM,
        JikanEndpointConsts.PEOPLE
    ];

    internal static IQuery Create()
    {
        return JikanQuery.Create(s_QueryEndpoint);
    }
}
