using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserSearchQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.USERS
    ];

    internal static IQuery Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.QUERY, query);
    }

    internal static IQuery Create(UserSearchQueryParameters parameters)
    {
        return JikanQuery.Create(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
