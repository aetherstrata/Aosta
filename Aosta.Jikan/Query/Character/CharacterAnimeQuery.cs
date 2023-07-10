using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Character;

internal sealed class CharacterAnimeQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString(),
        JikanEndpointConsts.Anime
    };

    private CharacterAnimeQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static CharacterAnimeQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new CharacterAnimeQuery(id);
    }
}