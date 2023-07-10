using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Person;

internal sealed class PersonFullDataQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.People,
        id.ToString(),
        JikanEndpointConsts.Full
    };

    private PersonFullDataQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static PersonFullDataQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new PersonFullDataQuery(id);
    }
}