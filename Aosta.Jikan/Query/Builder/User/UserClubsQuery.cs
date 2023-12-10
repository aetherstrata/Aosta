using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class UserClubsQuery
{
    private static string[] getEndpoint(string username) => new []
    {
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.CLUBS
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<MalUrlResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MalUrlResponse>>>(getEndpoint(username));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MalUrlResponse>>> Create(string username, int page)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MalUrlResponse>>>(getEndpoint(username))
            .WithParameter(QueryParameter.PAGE, page);
    }
}
