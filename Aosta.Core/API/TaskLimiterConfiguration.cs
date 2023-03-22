namespace Aosta.Core.API;

/// <summary> <see cref="TaskLimiter"/> configuration class </summary>
/// <seealso cref="CompositeTaskLimiter"/>
public class TaskLimiterConfiguration : IEquatable<TaskLimiterConfiguration>, IComparable<TaskLimiterConfiguration>, IComparable
{
    /// <summary> Maximum number of task executions per unit of time </summary>
    public required int Count { get; init; }

    /// <summary> Unit of time </summary>
    public required TimeSpan TimeSpan { get; init; }

    /// <summary> Maximum execution rate of the configuration </summary>
    /// <remarks> Rate is expressed in times per second </remarks>
    public double MaximumRate => Count / TimeSpan.TotalSeconds;

    internal static IEnumerable<TaskLimiterConfiguration> DefaultConfiguration { get; } = new List<TaskLimiterConfiguration>()
    {
        new TaskLimiterConfiguration()
        {
            Count = 1,
            TimeSpan = TimeSpan.FromMilliseconds(500)
        },
        new TaskLimiterConfiguration()
        {
            Count = 4,
            TimeSpan = TimeSpan.FromMilliseconds(4000)
        }
    };

    #region Equality methods

    public bool Equals(TaskLimiterConfiguration? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Count == other.Count && TimeSpan.Equals(other.TimeSpan);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TaskLimiterConfiguration)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Count, TimeSpan);
    }

    public static bool operator ==(TaskLimiterConfiguration? left, TaskLimiterConfiguration? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TaskLimiterConfiguration? left, TaskLimiterConfiguration? right)
    {
        return !Equals(left, right);
    }

    #endregion

    #region Comparator methods

    public int CompareTo(TaskLimiterConfiguration? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return MaximumRate.CompareTo(other.MaximumRate);
    }

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is TaskLimiterConfiguration other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(TaskLimiterConfiguration)}");
    }

    public static bool operator <(TaskLimiterConfiguration? left, TaskLimiterConfiguration? right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(TaskLimiterConfiguration? left, TaskLimiterConfiguration? right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(TaskLimiterConfiguration? left, TaskLimiterConfiguration? right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(TaskLimiterConfiguration? left, TaskLimiterConfiguration? right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) >= 0;
    }

    #endregion
}