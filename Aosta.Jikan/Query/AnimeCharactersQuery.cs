using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query;

internal sealed class AnimeCharactersQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Characters
    };

    private AnimeCharactersQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static AnimeCharactersQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new AnimeCharactersQuery(id);
    }
}