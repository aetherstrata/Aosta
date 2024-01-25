using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Top;

internal static class TopMangaQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.TOP_LIST,
        JikanEndpointConsts.MANGA
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

    internal static IQuery Create(TopMangaFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.FILTER, filter);
    }

    internal static IQuery Create(int page, TopMangaFilter filter)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.PAGE, page)
            .Add(QueryParameter.FILTER, filter);
    }

    internal static IQuery Create(TopMangaQueryParameters parameters)
    {
        return new JikanQuery(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
