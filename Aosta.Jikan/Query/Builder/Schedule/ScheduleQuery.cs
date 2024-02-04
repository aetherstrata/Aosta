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
        return JikanQuery.Create(s_QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.PAGE, page);
    }

    internal static IQuery Create(ScheduledDayFilter day)
    {
        Guard.IsValidEnum(day, nameof(day));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.FILTER, day);
    }

    internal static IQuery Create(ScheduleQueryParameters parameters)
    {
        return JikanQuery.Create(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
