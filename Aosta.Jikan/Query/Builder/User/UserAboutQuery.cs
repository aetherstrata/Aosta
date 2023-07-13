using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class UserAboutQuery
{
    private static string[] GetEndpoint(string username) => new []
    {
        JikanEndpointConsts.Users,
        username,
        JikanEndpointConsts.About
    };

    internal static IQuery<BaseJikanResponse<UserAboutResponse>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<BaseJikanResponse<UserAboutResponse>>(GetEndpoint(username));
    }
}