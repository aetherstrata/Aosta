using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class CharacterSearchQuery
{
    private static readonly string[] QueryEndpoint = new []
    {
        JikanEndpointConsts.Characters
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>> Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Query, query);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>> Create(CharacterSearchQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}