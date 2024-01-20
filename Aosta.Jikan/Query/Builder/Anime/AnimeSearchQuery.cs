using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeSearchQuery
{
    private static readonly string[] s_QueryEndpoint = new []
    {
        JikanEndpointConsts.ANIME
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.QUERY, query);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(AnimeSearchQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(s_QueryEndpoint)
            .WithParameterRange(parameters);
    }
}
