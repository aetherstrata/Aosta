using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserHistoryQuery
{
    private static string[] getEndpoint(string username) =>
    [
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.HISTORY
    ];

    internal static IQuery Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return JikanQuery.Create(getEndpoint(username));
    }

    internal static IQuery Create(string username, UserHistoryTypeFilter type)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsValidEnum(type, nameof(type));
        return JikanQuery.Create(getEndpoint(username))
            .With(QueryParameter.TYPE, type);
    }
}
