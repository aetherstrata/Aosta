using Aosta.Common.Extensions;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class SeasonQueryParameters : JikanQueryParameterSet
{
    public SeasonQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public SeasonQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        base.Add(QueryParameter.Limit, limit);
        return this;
    }

    public SeasonQueryParameters SetFilter(AnimeTypeFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        base.Add(QueryParameter.Filter, filter.FlagToString());
        return this;
    }

    public SeasonQueryParameters SetSafeForWork(bool sfw)
    {
        base.Add(QueryParameter.SafeForWork, sfw);
        return this;
    }

    public SeasonQueryParameters SetUnapproved(bool unapproved)
    {
        base.Add(QueryParameter.Unapproved, unapproved);
        return this;
    }
}