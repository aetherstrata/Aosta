using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeForumTopicsQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.FORUM
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return JikanQuery.Create(getEndpoint(id));
    }

    internal static IQuery Create(long id, ForumTopicTypeFilter type)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsValidEnum(type, nameof(type));
        return JikanQuery.Create(getEndpoint(id))
            .With(QueryParameter.FILTER, type);
    }
}
