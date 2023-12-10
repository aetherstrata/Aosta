using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class ClubRelationsQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.CLUBS,
        id.ToString(),
        JikanEndpointConsts.RELATIONS
    };

    internal static IQuery<BaseJikanResponse<ClubRelationsResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ClubRelationsResponse>>(getEndpoint(id));
    }
}
