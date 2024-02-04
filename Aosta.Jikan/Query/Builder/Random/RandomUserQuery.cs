namespace Aosta.Jikan.Query.Builder.Random;

internal static class RandomUserQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.RANDOM,
        JikanEndpointConsts.USERS
    ];

    internal static IQuery Create()
    {
        return JikanQuery.Create(s_QueryEndpoint);
    }
}
