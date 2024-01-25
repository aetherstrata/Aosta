using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserExternalLinksQuery
{
    private static string[] getEndpoint(string username) =>
    [
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.EXTERNAL
    ];

    internal static IQuery Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery(getEndpoint(username));
    }
}
