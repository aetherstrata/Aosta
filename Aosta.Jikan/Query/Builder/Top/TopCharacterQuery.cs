using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Top;

internal static class TopCharacterQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.TOP_LIST,
        JikanEndpointConsts.CHARACTERS
    ];

    internal static IQuery Create()
    {
        return new JikanQuery(s_QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.PAGE, page);
    }

    internal static IQuery Create(int page, int limit)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.PAGE, page)
            .Add(QueryParameter.LIMIT, limit);
    }
}
