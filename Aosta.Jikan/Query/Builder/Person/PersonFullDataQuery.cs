using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Person;

internal static class PersonFullDataQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.PEOPLE,
        id.ToString(),
        JikanEndpointConsts.FULL
    };

    internal static IQuery<BaseJikanResponse<PersonResponseFull>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<PersonResponseFull>>(getEndpoint(id));
    }
}
