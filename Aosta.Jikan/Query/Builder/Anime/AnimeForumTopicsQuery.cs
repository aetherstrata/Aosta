using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class AnimeForumTopicsQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.FORUM
    };

    internal static IQuery<BaseJikanResponse<ICollection<ForumTopicResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<ForumTopicResponse>>>(getEndpoint(id));
    }

    internal static IQuery<BaseJikanResponse<ICollection<ForumTopicResponse>>> Create(long id, ForumTopicTypeFilter type)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery<BaseJikanResponse<ICollection<ForumTopicResponse>>>(getEndpoint(id))
            .WithParameter(QueryParameter.FILTER, type);
    }
}
