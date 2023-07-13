using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class ClubRelationsQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Clubs,
        id.ToString(),
        JikanEndpointConsts.Relations
    };

    internal static IQuery<BaseJikanResponse<ClubRelationsResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ClubRelationsResponse>>(GetEndpoint(id));
    }
}