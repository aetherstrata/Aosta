using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserAnimeListQuery
{
    private static string[] getEndpoint(string username) => new []
    {
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.ANIME_LIST
    };

    internal static IQuery<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<BaseJikanResponse<ICollection<AnimeListEntryResponse>>>(getEndpoint(username));
    }

    internal static IQuery<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> Create(string username, UserAnimeStatusFilter status)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsValidEnum(status, nameof(status));
        return new JikanQuery<BaseJikanResponse<ICollection<AnimeListEntryResponse>>>(getEndpoint(username))
            .WithParameter(QueryParameter.STATUS, status);
    }
}
