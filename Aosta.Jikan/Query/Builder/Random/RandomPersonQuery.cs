using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class RandomPersonQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.People
    };

    internal static IQuery<BaseJikanResponse<PersonResponse>> Create()
    {
        return new JikanQuery<BaseJikanResponse<PersonResponse>>(QueryEndpoint);
    }
}