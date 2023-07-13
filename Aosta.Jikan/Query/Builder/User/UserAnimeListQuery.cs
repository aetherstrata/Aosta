using Aosta.Common.Extensions;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class UserAnimeListQuery
{
    private static string[] GetEndpoint(string username) => new []
    {
        JikanEndpointConsts.Users,
        username,
        JikanEndpointConsts.AnimeList
    };

    internal static IQuery<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<BaseJikanResponse<ICollection<AnimeListEntryResponse>>>(GetEndpoint(username));
    }

    internal static IQuery<BaseJikanResponse<ICollection<AnimeListEntryResponse>>> Create(string username, UserAnimeStatusFilter status)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsValidEnum(status, nameof(status));
        return new JikanQuery<BaseJikanResponse<ICollection<AnimeListEntryResponse>>>(GetEndpoint(username))
            .WithParameter(QueryParameter.Status, status.StringValue());
    }
}