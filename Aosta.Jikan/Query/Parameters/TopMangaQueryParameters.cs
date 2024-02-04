using Aosta.Common;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class TopMangaQueryParameters : JikanQueryParameterSet, IFactory<TopMangaQueryParameters>
{
    private TopMangaQueryParameters(){}

    public static TopMangaQueryParameters Create()
    {
        return new TopMangaQueryParameters();
    }

    public TopMangaQueryParameters Page(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public TopMangaQueryParameters Limit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public TopMangaQueryParameters Type(MangaTypeFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        Add(QueryParameter.TYPE, filter);
        return this;
    }

    public TopMangaQueryParameters Filter(TopMangaFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        Add(QueryParameter.FILTER, filter);
        return this;
    }
}
