using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class CharacterMangaQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString(),
        JikanEndpointConsts.Manga
    };

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new Query(GetEndpoint(id));
    }
}