using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserRecommendationsQuery
{
    private static string[] getEndpoint(string username) => new []
    {
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.RECOMMENDATIONS
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(getEndpoint(username));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> Create(string username, int page)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(getEndpoint(username))
            .WithParameter(QueryParameter.PAGE, page);
    }
}
