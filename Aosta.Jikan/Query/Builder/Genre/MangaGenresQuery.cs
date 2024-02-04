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
        return JikanQuery.Create(s_QueryEndpoint);
    }

    internal static IQuery Create(GenresFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.FILTER, filter);
    }
}
