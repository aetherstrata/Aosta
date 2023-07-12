using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class TopMangaQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.TopList,
        JikanEndpointConsts.Manga
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery Create(TopMangaFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Filter, filter);
    }

    internal static IQuery Create(int page, TopMangaFilter filter)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page)
            .WithParameter(QueryParameter.Filter, filter);
    }

    internal static IQuery Create(TopMangaQueryParameters parameters)
    {
        return new JikanQuery(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}