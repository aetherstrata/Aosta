using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class WatchRecentEpisodesQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Watch,
        JikanEndpointConsts.Episodes
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>>(QueryEndpoint);
    }
}