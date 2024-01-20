using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Club;

internal static class ClubStaffQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.CLUBS,
        id.ToString(),
        JikanEndpointConsts.STAFF
    };

    internal static IQuery<BaseJikanResponse<ICollection<ClubStaffResponse>>> Crete(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<ClubStaffResponse>>>(getEndpoint(id));
    }
}
