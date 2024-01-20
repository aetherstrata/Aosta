using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeStaffQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.STAFF
    };

    internal static IQuery<BaseJikanResponse<ICollection<AnimeStaffPositionResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<AnimeStaffPositionResponse>>>(getEndpoint(id));
    }
}
