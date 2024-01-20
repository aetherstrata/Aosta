using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserProfileQuery
{
    private static string[] getEndpoint(string username) => new []
    {
        JikanEndpointConsts.USERS,
        username
    };

    internal static IQuery<BaseJikanResponse<UserProfileResponse>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<BaseJikanResponse<UserProfileResponse>>(getEndpoint(username));
    }
}
