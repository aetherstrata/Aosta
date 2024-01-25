using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeUserUpdatesQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.USER_UPDATES
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }

    internal static IQuery Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(getEndpoint(id))
            .Add(QueryParameter.PAGE, page);
    }
}
