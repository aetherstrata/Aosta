using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class RandomCharacterQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Characters
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }
}