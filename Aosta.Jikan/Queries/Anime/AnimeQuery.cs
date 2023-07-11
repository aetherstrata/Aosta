using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class AnimeQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString()
    };

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new Query(GetEndpoint(id));
    }
}