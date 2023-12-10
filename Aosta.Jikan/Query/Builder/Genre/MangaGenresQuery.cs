using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class MangaGenresQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.GENRES,
        JikanEndpointConsts.MANGA
    };

    internal static IQuery<BaseJikanResponse<ICollection<GenreResponse>>> Create()
    {
        return new JikanQuery<BaseJikanResponse<ICollection<GenreResponse>>>(s_QueryEndpoint);
    }

    internal static IQuery<BaseJikanResponse<ICollection<GenreResponse>>> Create(GenresFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery<BaseJikanResponse<ICollection<GenreResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.FILTER, filter);
    }
}
