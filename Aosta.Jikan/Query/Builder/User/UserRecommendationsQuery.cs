namespace Aosta.Jikan.Query.Builder.User;

internal static class UserRecommendationsQuery
{
    private static string[] getEndpoint(string username) =>
    [
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.RECOMMENDATIONS
    ];

    internal static IQuery Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return JikanQuery.Create(getEndpoint(username));
    }

    internal static IQuery Create(string username, int page)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));
        return JikanQuery.Create(getEndpoint(username))
            .With(QueryParameter.PAGE, page);
    }
}
