using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class MangaGenresQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Genres,
        JikanEndpointConsts.Manga
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }

    internal static IQuery Create(GenresFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Filter, filter);
    }
}