namespace Aosta.Jikan.Query;

internal static class RandomUserQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Users
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }
}