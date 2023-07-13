using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class WatchPopularEpisodesQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Watch,
        JikanEndpointConsts.Episodes,
        JikanEndpointConsts.Popular
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<WatchEpisodeResponse>>>(QueryEndpoint);
    }
}