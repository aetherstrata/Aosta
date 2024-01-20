using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserFriendsQuery
{
    private static string[] getEndpoint(string username) => new []
    {
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.FRIENDS
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<FriendResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<PaginatedJikanResponse<ICollection<FriendResponse>>>(getEndpoint(username));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<FriendResponse>>> Create(string username, int page)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<FriendResponse>>>(getEndpoint(username))
            .WithParameter(QueryParameter.PAGE, page);
    }
}
