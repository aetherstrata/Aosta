using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class ClubMembersQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Clubs,
        id.ToString(),
        JikanEndpointConsts.Members
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ClubMemberResponse>>>(GetEndpoint(id));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ClubMemberResponse>>>(GetEndpoint(id))
            .WithParameter(QueryParameter.Page, page);
    }
}