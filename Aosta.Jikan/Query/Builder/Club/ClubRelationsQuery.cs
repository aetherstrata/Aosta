using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Club;

internal static class ClubRelationsQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.CLUBS,
        id.ToString(),
        JikanEndpointConsts.RELATIONS
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
