namespace Aosta.Jikan.Query.Builder.User;

internal static class UserFriendsQuery
{
    private static string[] getEndpoint(string username) =>
    [
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.FRIENDS
    ];

    internal static IQuery Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery(getEndpoint(username));
    }

    internal static IQuery Create(string username, int page)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(getEndpoint(username))
            .Add(QueryParameter.PAGE, page);
    }
}
