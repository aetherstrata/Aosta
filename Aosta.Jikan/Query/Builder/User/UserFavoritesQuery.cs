namespace Aosta.Jikan.Query.Builder.User;

internal static class UserFavoritesQuery
{
    private static string[] getEndpoint(string username) =>
    [
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.FAVORITES
    ];

    internal static IQuery Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return JikanQuery.Create(getEndpoint(username));
    }
}
