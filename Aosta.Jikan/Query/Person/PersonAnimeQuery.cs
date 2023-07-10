using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Person;

internal sealed class PersonAnimeQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.People,
        id.ToString(),
        JikanEndpointConsts.Anime
    };

    private PersonAnimeQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static PersonAnimeQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new PersonAnimeQuery(id);
    }
}