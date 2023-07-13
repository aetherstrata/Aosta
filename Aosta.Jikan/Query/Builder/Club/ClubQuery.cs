using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class ClubQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Clubs,
        id.ToString()
    };

    internal static IQuery<BaseJikanResponse<ClubResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ClubResponse>>(GetEndpoint(id));
    }
}