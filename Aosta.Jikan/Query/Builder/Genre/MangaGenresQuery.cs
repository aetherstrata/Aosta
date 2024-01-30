using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Builder.Genre;

internal static class MangaGenresQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.GENRES,
        JikanEndpointConsts.MANGA
    ];

    internal static IQuery Create()
    {
        return new JikanQuery(s_QueryEndpoint);
    }

    internal static IQuery Create(GenresFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.FILTER, filter);
    }
}
