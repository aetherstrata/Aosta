using Aosta.Jikan.Consts;
using Aosta.Jikan.Enums;
using Aosta.Utils.Extensions;

namespace Aosta.Jikan.Queries;

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
        return new Query(GetEndpoint(id));
    }

    internal static IQuery Create(long id, ForumTopicType type)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsValidEnum(type, nameof(type));
        return new Query(GetEndpoint(id))
            .WithParameter(QueryParameter.Filter, type.StringValue());
    }
}