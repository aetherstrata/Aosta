using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
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
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.QUERY, query);
    }

    internal static IQuery Create(MangaSearchQueryParameters parameters)
    {
        return new JikanQuery(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
