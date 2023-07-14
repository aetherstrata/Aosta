using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class AnimeSearchQuery
{
    private static readonly string[] QueryEndpoint = new []
    {
        JikanEndpointConsts.Anime
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Query, query);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(AnimeSearchQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}