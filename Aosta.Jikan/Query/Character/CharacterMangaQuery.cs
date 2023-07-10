using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Character;

internal sealed class CharacterMangaQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString(),
        JikanEndpointConsts.Manga
    };

    private CharacterMangaQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static CharacterMangaQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new CharacterMangaQuery(id);
    }
}