using Aosta.Jikan.Enums;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class AnimeGenresQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Genres,
        JikanEndpointConsts.Anime
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