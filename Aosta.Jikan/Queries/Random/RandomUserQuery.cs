using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class RandomUserQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Users
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }
}