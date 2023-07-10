using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query;

internal sealed class AnimeNewsQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.News
    };

    private AnimeNewsQuery(long id) : base(QueryEndpoint(id))
    {
    }

    private AnimeNewsQuery(long id, int page) : this(id)
    {
        Parameters.Add(new QueryParameter<int>
        {
            Name = QueryParameter.Page,
            Value = page
        });
    }

    internal static AnimeNewsQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new AnimeNewsQuery(id);
    }

    internal static AnimeNewsQuery Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new AnimeNewsQuery(id, page);
    }
}