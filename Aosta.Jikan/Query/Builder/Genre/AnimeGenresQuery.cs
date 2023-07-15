using Aosta.Common.Extensions;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class AnimeGenresQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Genres,
        JikanEndpointConsts.Anime
    };

    internal static IQuery<BaseJikanResponse<ICollection<GenreResponse>>> Create()
    {
        return new JikanQuery<BaseJikanResponse<ICollection<GenreResponse>>>(QueryEndpoint);
    }
    
    internal static IQuery<BaseJikanResponse<ICollection<GenreResponse>>> Create(GenresFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery<BaseJikanResponse<ICollection<GenreResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Filter, filter);
    }
}