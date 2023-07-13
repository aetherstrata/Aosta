using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class TopCharacterQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.TopList,
        JikanEndpointConsts.Characters
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>>(QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>> Create(int page, int limit)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        return new JikanQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page)
            .WithParameter(QueryParameter.Limit, limit);
    }
}