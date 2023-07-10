using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query;

internal sealed class AnimeQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString()
    };

    private AnimeQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static AnimeQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new AnimeQuery(id);
    }
}