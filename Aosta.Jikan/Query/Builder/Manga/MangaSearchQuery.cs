using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Manga;

internal static class MangaSearchQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.MANGA
    ];

    internal static IQuery Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.QUERY, query);
    }

    internal static IQuery Create(MangaSearchQueryParameters parameters)
    {
        return JikanQuery.Create(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
