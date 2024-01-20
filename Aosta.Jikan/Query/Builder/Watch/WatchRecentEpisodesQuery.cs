using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Watch;

internal static class WatchRecentEpisodesQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.WATCH,
        JikanEndpointConsts.EPISODES
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>>(s_QueryEndpoint);
    }
}
