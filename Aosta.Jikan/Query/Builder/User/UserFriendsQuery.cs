using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class UserFriendsQuery
{
    private static string[] GetEndpoint(string username) => new []
    {
        JikanEndpointConsts.Users,
        username,
        JikanEndpointConsts.Friends
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<FriendResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<PaginatedJikanResponse<ICollection<FriendResponse>>>(GetEndpoint(username));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<FriendResponse>>> Create(string username, int page)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<FriendResponse>>>(GetEndpoint(username))
            .WithParameter(QueryParameter.Page, page);
    }
}