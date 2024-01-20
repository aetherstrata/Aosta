using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Person;

internal static class PersonQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.PEOPLE,
        id.ToString()
    };

    internal static IQuery<BaseJikanResponse<PersonResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<PersonResponse>>(getEndpoint(id));
    }
}
