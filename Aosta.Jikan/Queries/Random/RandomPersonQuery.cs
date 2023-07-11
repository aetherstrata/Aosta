using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class RandomPersonQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.People
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }
}