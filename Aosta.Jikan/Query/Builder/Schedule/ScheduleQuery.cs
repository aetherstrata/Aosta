using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Schedule;

internal static class ScheduleQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.SCHEDULES
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(s_QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(ScheduledDayFilter day)
    {
        Guard.IsValidEnum(day, nameof(day));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.FILTER, day);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(ScheduleQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(s_QueryEndpoint)
            .WithParameterRange(parameters);
    }
}
