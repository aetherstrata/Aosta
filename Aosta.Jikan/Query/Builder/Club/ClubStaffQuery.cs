using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class ClubStaffQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Clubs,
        id.ToString(),
        JikanEndpointConsts.Staff
    };

    internal static IQuery<BaseJikanResponse<ICollection<ClubStaffResponse>>> Crete(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<ClubStaffResponse>>>(GetEndpoint(id));
    }
}