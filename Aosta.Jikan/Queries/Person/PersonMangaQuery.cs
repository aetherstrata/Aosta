using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class PersonMangaQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.People,
        id.ToString(),
        JikanEndpointConsts.Manga
    };

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new Query(GetEndpoint(id));
    }
}