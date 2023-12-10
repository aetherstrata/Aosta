using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class UserHistoryQuery
{
    private static string[] GetEndpoint(string username) => new []
    {
        JikanEndpointConsts.Users,
        username,
        JikanEndpointConsts.History
    };

    internal static IQuery<BaseJikanResponse<ICollection<HistoryEntryResponse>>> Create(string username)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        return new JikanQuery<BaseJikanResponse<ICollection<HistoryEntryResponse>>>(GetEndpoint(username));
    }

    internal static IQuery<BaseJikanResponse<ICollection<HistoryEntryResponse>>> Create(string username, UserHistoryTypeFilter type)
    {
        Guard.IsNotNullOrWhiteSpace(username, nameof(username));
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery<BaseJikanResponse<ICollection<HistoryEntryResponse>>>(GetEndpoint(username))
            .WithParameter(QueryParameter.Type, type);
    }
}