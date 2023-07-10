using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Person;

internal sealed class PersonPicturesQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.People,
        id.ToString(),
        JikanEndpointConsts.Pictures
    };

    private PersonPicturesQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static PersonPicturesQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new PersonPicturesQuery(id);
    }
}