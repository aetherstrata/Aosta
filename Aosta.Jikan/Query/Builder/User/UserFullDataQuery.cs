namespace Aosta.Jikan.Query.Builder.User;

internal static class UserFullDataQuery
{
    private static string[] getEndpoint(string username) =>
    [
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.FULL
    ];

    internal static IQuery Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return JikanQuery.Create(getEndpoint(username));
    }
}
