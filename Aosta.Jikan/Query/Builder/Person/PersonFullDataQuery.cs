using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class PersonFullDataQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.People,
        id.ToString(),
        JikanEndpointConsts.Full
    };

    internal static IQuery<BaseJikanResponse<PersonResponseFull>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<PersonResponseFull>>(GetEndpoint(id));
    }
}