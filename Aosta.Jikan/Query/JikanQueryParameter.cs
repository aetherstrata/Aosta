using FastEnumUtility;

namespace Aosta.Jikan.Query;

/// <summary>
/// A single query parameter.
/// </summary>
/// <typeparam name="T">Type of the query parameter value.</typeparam>
internal class JikanQueryParameter<T> : IQueryParameter, IEquatable<JikanQueryParameter<T>>
{
    /// <summary>
    /// Name of the query parameter.
    /// </summary>
    internal required string Name { get; init; }

    /// <summary>
    /// Value of the query parameter.
    /// </summary>
    internal required T Value { get; init; }

    string IQueryParameter.GetName() => Name;

    public override string ToString() => $"{Name}={Value}";

    public bool Equals(JikanQueryParameter<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((JikanQueryParameter<T>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Value);
    }
}

/// <summary>
/// Utility class for query parameters
/// </summary>
internal static class QueryParameter
{
    public const string Filter = "filter";
    public const string Kids = "kids";
    public const string Letter = "letter";
    public const string Limit = "limit";
    public const string OrderBy = "order_by";
    public const string Page = "page";
    public const string Preliminary = "preliminary";
    public const string Query = "q";
    public const string Rating = "rating";
    public const string SafeForWork = "sfw";
    public const string Sort = "sort";
    public const string Spoilers = "spoilers";
    public const string Status = "status";
    public const string Type = "type";
    public const string Unapproved = "unapproved";

    internal static IEqualityComparer<IQueryParameter> NameComparer { get; } = new NameEqualityComparer();

    private sealed class NameEqualityComparer : IEqualityComparer<IQueryParameter>
    {
        public bool Equals(IQueryParameter? x, IQueryParameter? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.GetName() == y.GetName();
        }

        public int GetHashCode(IQueryParameter obj)
        {
            return obj.GetName().GetHashCode();
        }
    }
}
