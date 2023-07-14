using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class PersonSearchQuery
{
    private static readonly string[] QueryEndpoint = new []
    {
        JikanEndpointConsts.People
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<PersonResponse>>> Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<PersonResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Query, query);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<PersonResponse>>> Create(PersonSearchQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<PersonResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}