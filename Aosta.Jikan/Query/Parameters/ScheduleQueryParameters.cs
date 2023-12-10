using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class ScheduleQueryParameters : JikanQueryParameterSet
{
    public ScheduleQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public ScheduleQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public ScheduleQueryParameters SetFilter(ScheduledDayFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        Add(QueryParameter.FILTER, filter);
        return this;
    }

    public ScheduleQueryParameters SetKids(bool kids)
    {
        Add(QueryParameter.KIDS, kids);
        return this;
    }

    public ScheduleQueryParameters SetSafeForWork(bool sfw)
    {
        Add(QueryParameter.SAFE_FOR_WORK, sfw);
        return this;
    }

    public ScheduleQueryParameters SetUnapproved(bool unapproved)
    {
        Add(QueryParameter.UNAPPROVED, unapproved);
        return this;
    }
}
