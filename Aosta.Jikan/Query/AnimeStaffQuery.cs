using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query;

internal sealed class AnimeStaffQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Staff
    };

    private AnimeStaffQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static AnimeStaffQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new AnimeStaffQuery(id);
    }
}