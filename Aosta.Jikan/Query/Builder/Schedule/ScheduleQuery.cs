using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Schedule;

internal static class ScheduleQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.SCHEDULES
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

    internal static IQuery Create(ScheduledDayFilter day)
    {
        Guard.IsValidEnum(day, nameof(day));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.FILTER, day);
    }

    internal static IQuery Create(ScheduleQueryParameters parameters)
    {
        return new JikanQuery(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
