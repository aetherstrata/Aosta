using Aosta.Common;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class ScheduleQueryParameters : JikanQueryParameterSet, IFactory<ScheduleQueryParameters>
{
    private ScheduleQueryParameters(){}

    public static ScheduleQueryParameters Create()
    {
        return new ScheduleQueryParameters();
    }

    public ScheduleQueryParameters Page(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public ScheduleQueryParameters Limit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public ScheduleQueryParameters Filter(ScheduledDayFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        Add(QueryParameter.FILTER, filter);
        return this;
    }

    public ScheduleQueryParameters Kids(bool kids)
    {
        Add(QueryParameter.KIDS, kids);
        return this;
    }

    public ScheduleQueryParameters SafeForWork(bool sfw)
    {
        Add(QueryParameter.SAFE_FOR_WORK, sfw);
        return this;
    }

    public ScheduleQueryParameters Unapproved(bool unapproved)
    {
        Add(QueryParameter.UNAPPROVED, unapproved);
        return this;
    }
}
