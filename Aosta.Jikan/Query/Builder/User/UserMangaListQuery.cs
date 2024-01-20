using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Builder.User;

internal static class UserMangaListQuery
{
    private static string[] getEndpoint(string username) => new []
    {
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.MANGA_LIST
    };

    internal static IQuery<BaseJikanResponse<ICollection<MangaListEntryResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<BaseJikanResponse<ICollection<MangaListEntryResponse>>>(getEndpoint(username));
    }

    internal static IQuery<BaseJikanResponse<ICollection<MangaListEntryResponse>>> Create(string username, UserMangaStatusFilter status)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsValidEnum(status, nameof(status));
        return new JikanQuery<BaseJikanResponse<ICollection<MangaListEntryResponse>>>(getEndpoint(username))
            .WithParameter(QueryParameter.STATUS, status);
    }
}
