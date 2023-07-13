using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class SeasonArchiveQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Seasons
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<SeasonArchiveResponse>>>(QueryEndpoint);
    }
}