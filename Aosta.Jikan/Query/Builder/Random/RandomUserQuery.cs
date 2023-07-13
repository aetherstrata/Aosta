using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class RandomUserQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Users
    };

    internal static IQuery<BaseJikanResponse<UserProfileResponse>> Create()
    {
        return new JikanQuery<BaseJikanResponse<UserProfileResponse>>(QueryEndpoint);
    }
}