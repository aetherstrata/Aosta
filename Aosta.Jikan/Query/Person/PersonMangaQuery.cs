using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Person;

internal sealed class PersonMangaQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.People,
        id.ToString(),
        JikanEndpointConsts.Manga
    };

    private PersonMangaQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static PersonMangaQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new PersonMangaQuery(id);
    }
}