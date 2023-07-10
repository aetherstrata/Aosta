using FastEnumUtility;

namespace Aosta.Jikan.Query;

internal static class QueryParameter
{
    public const string Page = "page";
    public const string Limit = "limit";
    public const string Filter = "filter";
    public const string Kids = "kids";
    public const string SafeForWork = "sfw";
    public const string Unapproved = "unapproved";
}

internal class QueryParameter<T> : IQueryParameter
{
    internal required string Name { get; init; }

    internal required T Value { get; init; }

    public override string ToString() => Value switch
    {
        true => Name,
        false => string.Empty,
        _ => $"{Name}={Value}"
    };
}

