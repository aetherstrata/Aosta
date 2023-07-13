using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class UserRecommendationsQuery
{
    private static string[] GetEndpoint(string username) => new []
    {
        JikanEndpointConsts.Users,
        username,
        JikanEndpointConsts.Recommendations
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(GetEndpoint(username));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> Create(string username, int page)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(GetEndpoint(username))
            .WithParameter(QueryParameter.Page, page);
    }
}