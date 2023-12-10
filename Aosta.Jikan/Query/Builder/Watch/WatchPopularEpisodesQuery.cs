using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class WatchPopularEpisodesQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.WATCH,
        JikanEndpointConsts.EPISODES,
        JikanEndpointConsts.POPULAR
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>>(s_QueryEndpoint);
    }
}
