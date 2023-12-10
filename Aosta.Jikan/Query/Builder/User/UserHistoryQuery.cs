using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class UserHistoryQuery
{
    private static string[] getEndpoint(string username) => new []
    {
        JikanEndpointConsts.USERS,
        username,
        JikanEndpointConsts.HISTORY
    };

    internal static IQuery<BaseJikanResponse<ICollection<HistoryEntryResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<BaseJikanResponse<ICollection<HistoryEntryResponse>>>(getEndpoint(username));
    }

    internal static IQuery<BaseJikanResponse<ICollection<HistoryEntryResponse>>> Create(string username, UserHistoryTypeFilter type)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery<BaseJikanResponse<ICollection<HistoryEntryResponse>>>(getEndpoint(username))
            .WithParameter(QueryParameter.TYPE, type);
    }
}
