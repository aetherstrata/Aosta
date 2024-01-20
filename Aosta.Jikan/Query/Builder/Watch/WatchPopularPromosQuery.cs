using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Watch;

internal static class WatchPopularPromosQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.WATCH,
        JikanEndpointConsts.PROMOS,
        JikanEndpointConsts.POPULAR
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<WatchPromoVideoResponse>>>(s_QueryEndpoint);
    }
}
