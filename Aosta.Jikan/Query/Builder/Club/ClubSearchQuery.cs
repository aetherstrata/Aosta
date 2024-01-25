using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class ClubSearchQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.CLUBS
    ];

    internal static IQuery Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.QUERY, query);
    }

    internal static IQuery Create(ClubSearchQueryParameters parameters)
    {
        return new JikanQuery(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
