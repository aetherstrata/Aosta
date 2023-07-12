using Aosta.Jikan.Enums;
using Aosta.Common.Extensions;

namespace Aosta.Jikan.Query;

internal static class AnimeForumTopicsQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Forum
    };

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(GetEndpoint(id));
    }

    internal static IQuery Create(long id, ForumTopicType type)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery(GetEndpoint(id))
            .WithParameter(QueryParameter.Filter, type.StringValue());
    }
}