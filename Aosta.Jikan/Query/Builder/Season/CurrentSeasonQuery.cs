using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Season;

internal static class CurrentSeasonQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.SEASONS,
        JikanEndpointConsts.NOW
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

    internal static IQuery Create(SeasonQueryParameters parameters)
    {
        return JikanQuery.Create(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
