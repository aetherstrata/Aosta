using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Person;

internal static class PersonPicturesQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.PEOPLE,
        id.ToString(),
        JikanEndpointConsts.PICTURES
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
