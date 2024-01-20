using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserReviewsQuery
{
    private static string[] getEndpoint(string username) => new []
    {
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.REVIEWS
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(getEndpoint(username));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(string username, int page)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(getEndpoint(username))
            .WithParameter(QueryParameter.PAGE, page);
    }
}
