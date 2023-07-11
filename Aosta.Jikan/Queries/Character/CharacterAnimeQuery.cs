using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class CharacterAnimeQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString(),
        JikanEndpointConsts.Anime
    };
    
    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new Query(GetEndpoint(id));
    }
}