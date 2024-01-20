using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Season;

internal static class SeasonArchiveQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.SEASONS
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>>(s_QueryEndpoint);
    }
}
