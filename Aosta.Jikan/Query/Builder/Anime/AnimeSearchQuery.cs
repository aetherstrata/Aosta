using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeSearchQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.ANIME
    ];

    internal static IQuery Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.QUERY, query);
    }

    internal static IQuery Create(AnimeSearchQueryParameters parameters)
    {
        return new JikanQuery(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
