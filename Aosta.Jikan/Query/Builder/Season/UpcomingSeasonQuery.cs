namespace Aosta.Jikan.Query;

internal static class UpcomingSeasonQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Seasons,
        JikanEndpointConsts.Upcoming
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery Create(SeasonQueryParameters parameters)
    {
        return new JikanQuery(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}