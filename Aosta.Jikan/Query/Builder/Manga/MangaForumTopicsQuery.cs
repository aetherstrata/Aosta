using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Manga;

internal static class MangaForumTopicsQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.MANGA,
        id.ToString(),
        JikanEndpointConsts.FORUM
    };

    internal static IQuery<BaseJikanResponse<ICollection<ForumTopicResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<ForumTopicResponse>>>(getEndpoint(id));
    }
}
