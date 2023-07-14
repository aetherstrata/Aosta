using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class UserSearchQuery
{
    private static readonly string[] QueryEndpoint = new []
    {
        JikanEndpointConsts.Users
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<UserMetadataResponse>>> Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<UserMetadataResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Query, query);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<UserMetadataResponse>>> Create(UserSearchQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<UserMetadataResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}
