using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Person;

internal static class PersonSearchQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.PEOPLE
    ];

    internal static IQuery Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.QUERY, query);
    }

    internal static IQuery Create(PersonSearchQueryParameters parameters)
    {
        return JikanQuery.Create(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
