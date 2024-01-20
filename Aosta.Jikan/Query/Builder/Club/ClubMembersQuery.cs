using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Club;

internal static class ClubMembersQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.CLUBS,
        id.ToString(),
        JikanEndpointConsts.MEMBERS
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ClubMemberResponse>>>(getEndpoint(id));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ClubMemberResponse>>> Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ClubMemberResponse>>>(getEndpoint(id))
            .WithParameter(QueryParameter.PAGE, page);
    }
}
