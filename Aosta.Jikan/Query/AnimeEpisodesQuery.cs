using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query;

internal sealed class AnimeEpisodesQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Episodes
    };

    private AnimeEpisodesQuery(long id) : base(QueryEndpoint(id))
    {
    }

    private AnimeEpisodesQuery(long id, int page) : this(id)
    {
        Parameters.Add(new QueryParameter<int>()
        {
            Name = QueryParameter.Page,
            Value = page
        });
    }

    internal static AnimeEpisodesQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new AnimeEpisodesQuery(id);
    }

    internal static AnimeEpisodesQuery Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new AnimeEpisodesQuery(id, page);
    }
}