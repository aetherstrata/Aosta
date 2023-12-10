using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class UserMangaListQuery
{
    private static string[] GetEndpoint(string username) => new []
    {
        JikanEndpointConsts.Users,
        username,
        JikanEndpointConsts.MangaList
    };

    internal static IQuery<BaseJikanResponse<ICollection<MangaListEntryResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<BaseJikanResponse<ICollection<MangaListEntryResponse>>>(GetEndpoint(username));
    }

    internal static IQuery<BaseJikanResponse<ICollection<MangaListEntryResponse>>> Create(string username, UserMangaStatusFilter status)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsValidEnum(status, nameof(status));
        return new JikanQuery<BaseJikanResponse<ICollection<MangaListEntryResponse>>>(GetEndpoint(username))
            .WithParameter(QueryParameter.Status, status);
    }
}