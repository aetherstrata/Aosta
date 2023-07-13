using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class WatchRecentPromosQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Watch,
        JikanEndpointConsts.Promos
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>>(QueryEndpoint);
    }
}