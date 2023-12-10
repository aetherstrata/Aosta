using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class RandomUserQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.RANDOM,
        JikanEndpointConsts.USERS
    };

    internal static IQuery<BaseJikanResponse<UserProfileResponse>> Create()
    {
        return new JikanQuery<BaseJikanResponse<UserProfileResponse>>(s_QueryEndpoint);
    }
}
