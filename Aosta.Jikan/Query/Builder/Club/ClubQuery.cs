using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Club;

internal static class ClubQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.CLUBS,
        id.ToString()
    };

    internal static IQuery<BaseJikanResponse<ClubResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ClubResponse>>(getEndpoint(id));
    }
}
