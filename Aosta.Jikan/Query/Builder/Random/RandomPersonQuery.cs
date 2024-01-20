using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Random;

internal static class RandomPersonQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.RANDOM,
        JikanEndpointConsts.PEOPLE
    };

    internal static IQuery<BaseJikanResponse<PersonResponse>> Create()
    {
        return new JikanQuery<BaseJikanResponse<PersonResponse>>(s_QueryEndpoint);
    }
}
