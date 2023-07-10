using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query;

internal sealed class AnimePicturesQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Pictures
    };

    private AnimePicturesQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static AnimePicturesQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new AnimePicturesQuery(id);
    }
}