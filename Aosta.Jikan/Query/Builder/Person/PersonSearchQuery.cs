using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class PersonSearchQuery
{
    private static readonly string[] s_QueryEndpoint = new []
    {
        JikanEndpointConsts.PEOPLE
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<PersonResponse>>> Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<PersonResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.QUERY, query);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<PersonResponse>>> Create(PersonSearchQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<PersonResponse>>>(s_QueryEndpoint)
            .WithParameterRange(parameters);
    }
}
