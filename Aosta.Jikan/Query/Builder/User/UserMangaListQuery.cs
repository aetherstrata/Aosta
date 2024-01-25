using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserMangaListQuery
{
    private static string[] getEndpoint(string username) =>
    [
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.MANGA_LIST
    ];

    internal static IQuery Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery(getEndpoint(username));
    }

    internal static IQuery Create(string username, UserMangaStatusFilter status)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsValidEnum(status, nameof(status));
        return new JikanQuery(getEndpoint(username))
            .Add(QueryParameter.STATUS, status);
    }
}
