using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Watch;

internal static class WatchRecentPromosQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.WATCH,
        JikanEndpointConsts.PROMOS
    ];

    internal static IQuery Create()
    {
        return new JikanQuery(s_QueryEndpoint);
    }
}
