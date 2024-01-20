using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserFullDataQuery
{
    private static string[] getEndpoint(string username) => new []
    {
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.FULL
    };

    internal static IQuery<BaseJikanResponse<UserResponseFull>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<BaseJikanResponse<UserResponseFull>>(getEndpoint(username));
    }
}
