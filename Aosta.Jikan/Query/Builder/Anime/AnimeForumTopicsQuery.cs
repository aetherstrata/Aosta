using Aosta.Jikan.Enums;
using Aosta.Common.Extensions;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class AnimeForumTopicsQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Forum
    };

    internal static IQuery<BaseJikanResponse<ICollection<ForumTopicResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<ForumTopicResponse>>>(GetEndpoint(id));
    }

    internal static IQuery<BaseJikanResponse<ICollection<ForumTopicResponse>>> Create(long id, ForumTopicType type)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery<BaseJikanResponse<ICollection<ForumTopicResponse>>>(GetEndpoint(id))
            .WithParameter(QueryParameter.Filter, type.StringValue());
    }
}