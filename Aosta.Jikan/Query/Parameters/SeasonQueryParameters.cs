using Aosta.Common;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class SeasonQueryParameters : JikanQueryParameterSet, IFactory<SeasonQueryParameters>
{
    private SeasonQueryParameters(){}

    public static SeasonQueryParameters Create()
    {
        return new SeasonQueryParameters();
    }

    public SeasonQueryParameters Page(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public SeasonQueryParameters Limit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public SeasonQueryParameters Filter(AnimeTypeFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        Add(QueryParameter.FILTER, filter);
        return this;
    }

    public SeasonQueryParameters SafeForWork(bool sfw)
    {
        Add(QueryParameter.SAFE_FOR_WORK, sfw);
        return this;
    }

    public SeasonQueryParameters Unapproved(bool unapproved)
    {
        Add(QueryParameter.UNAPPROVED, unapproved);
        return this;
    }
}
