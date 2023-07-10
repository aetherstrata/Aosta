using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Person;

internal sealed class PersonQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.People,
        id.ToString()
    };

    private PersonQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static PersonQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new PersonQuery(id);
    }
}