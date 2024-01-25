using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
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
        return new JikanQuery(s_QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.PAGE, page);
    }

    internal static IQuery Create(SeasonQueryParameters parameters)
    {
        return new JikanQuery(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
