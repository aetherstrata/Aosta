using Aosta.Common.Extensions;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class TopMangaQueryParameters : JikanQueryParameterSet
{
    public TopMangaQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public TopMangaQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        base.Add(QueryParameter.Limit, limit);
        return this;
    }

    public TopMangaQueryParameters SetType(MangaTypeFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        base.Add(QueryParameter.Type, filter.StringValue());
        return this;
    }

    public TopMangaQueryParameters SetFilter(TopMangaFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        base.Add(QueryParameter.Filter, filter.StringValue());
        return this;
    }
}