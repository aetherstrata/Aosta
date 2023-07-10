using Aosta.Jikan.Consts;
using Aosta.Jikan.Enums;
using FastEnumUtility;

namespace Aosta.Jikan.Query;

internal sealed class AnimeForumTopicsQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Forum
    };

    private AnimeForumTopicsQuery(long id) : base(QueryEndpoint(id))
    {
    }

    private AnimeForumTopicsQuery(long id, ForumTopicType type) : this(id)
    {
        Parameters.Add(new QueryParameter<string>
        {
            Name = QueryParameter.Filter,
            Value = type.GetEnumMemberValue()
        });
    }

    internal static AnimeForumTopicsQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new AnimeForumTopicsQuery(id);
    }

    internal static AnimeForumTopicsQuery Create(long id, ForumTopicType type)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsValidEnum(type, nameof(type));
        return new AnimeForumTopicsQuery(id, type);
    }
}