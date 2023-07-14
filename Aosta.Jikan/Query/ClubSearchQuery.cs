using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class ClubSearchQuery
{
    private static readonly string[] QueryEndpoint = new []
    {
        JikanEndpointConsts.Clubs
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<ClubResponse>>> Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ClubResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Query, query);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ClubResponse>>> Create(ClubSearchQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<ClubResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}