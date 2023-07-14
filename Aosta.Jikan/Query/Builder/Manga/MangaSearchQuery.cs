using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class MangaSearchQuery
{
    private static readonly string[] QueryEndpoint = new []
    {
        JikanEndpointConsts.Manga
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Query, query);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create(MangaSearchQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}