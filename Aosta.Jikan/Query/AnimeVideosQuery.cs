using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query;

internal sealed class AnimeVideosQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Videos
    };

    private AnimeVideosQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static AnimeVideosQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new AnimeVideosQuery(id);
    }
}