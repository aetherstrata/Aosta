using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserStatisticsQuery
{
    private static string[] getEndpoint(string username) => new []
    {
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.STATISTICS
    };

    internal static IQuery<BaseJikanResponse<UserStatisticsResponse>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<BaseJikanResponse<UserStatisticsResponse>>(getEndpoint(username));
    }
}
