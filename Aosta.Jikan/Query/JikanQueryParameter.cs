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
        return GetType() == obj.GetType() && Equals((JikanQueryParameter<T>)obj);
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
    public const string END_DATE = "end_date";
    public const string EXCLUDED_GENRES = "genres_exclude";
    public const string FILTER = "filter";
    public const string GENDER = "gender";
    public const string GENRES = "genres";
    public const string KIDS = "kids";
    public const string LETTER = "letter";
    public const string LIMIT = "limit";
    public const string MAX_AGE = "max_age";
    public const string MAX_SCORE = "max_score";
    public const string MIN_AGE = "min_age";
    public const string MIN_SCORE = "min_score";
    public const string ORDER_BY = "order_by";
    public const string PAGE = "page";
    public const string PRELIMINARY = "preliminary";
    public const string PRODUCERS = "producers";
    public const string QUERY = "q";
    public const string RATING = "rating";
    public const string SAFE_FOR_WORK = "sfw";
    public const string SCORE = "score";
    public const string SORT = "sort";
    public const string START_DATE = "start_date";
    public const string SPOILERS = "spoilers";
    public const string STATUS = "status";
    public const string TYPE = "type";
    public const string UNAPPROVED = "unapproved";

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
