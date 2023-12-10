using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class TopCharacterQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.TOP_LIST,
        JikanEndpointConsts.CHARACTERS
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>>(s_QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>> Create(int page, int limit)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        return new JikanQuery<PaginatedJikanResponse<ICollection<CharacterResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page)
            .WithParameter(QueryParameter.LIMIT, limit);
    }
}
