using Aosta.Jikan.Enums;
using Aosta.Common.Extensions;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class ScheduleQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Schedules
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(ScheduledDayFilter day)
    {
        Guard.IsValidEnum(day, nameof(day));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Filter, day.StringValue());
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(ScheduleQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}