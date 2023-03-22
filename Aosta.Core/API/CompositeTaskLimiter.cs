using Aosta.Core.Extensions;

namespace Aosta.Core.API;

/// <summary>
/// A composite task limiter that throttles task executions by chaining task execution to its <see cref="Limiters">TaskLimiters</see>.
/// The <see cref="Task"/> will execute only when all <see cref="SemaphoreSlim"/> have a free slot.
/// <para/>
/// Concept example:
/// <code>limiter1.LimitAsync(() => limiter2.LimitAsync(() => ExpensiveMethod()))</code>
/// </summary>
internal class CompositeTaskLimiter : ITaskLimiter, IEquatable<CompositeTaskLimiter>
{
    /// <summary> List of <see cref="TaskLimiter">Task Limiters</see> </summary>
    private List<TaskLimiter> Limiters { get; } = new();

    internal CompositeTaskLimiter(IEnumerable<TaskLimiterConfiguration> limiterConfigs)
    {
        limiterConfigs.ForEach(config => Limiters.Add(new TaskLimiter(config)));
    }

    /// <summary>
    /// Throttle the passed task execution according to the component <see cref="Limiters"/>
    /// </summary>
    /// <param name="taskFactory">Delegate that represents a method that returns a <see cref="Task"/></param>
    /// <returns> The input <see cref="Task"/> wrapped inside all <see cref="Limiters"/></returns>
    public Task LimitAsync(Func<Task> taskFactory)
    {
        foreach (var limiter in Limiters)
        {
            // Allocation is necessary otherwise it would point itself indefinitely
            var tmp = taskFactory;
            taskFactory = () => limiter.LimitAsync(tmp);
        }

        return taskFactory();
    }

    /// <summary>
    /// Throttle the passed task execution according to the component <see cref="Limiters"/>
    /// </summary>
    /// <param name="taskFactory">Delegate that represents a method that returns a <see cref="Task{T}"/></param>
    /// <returns> The input <see cref="Task{T}"/> wrapped inside all <see cref="Limiters"/></returns>
    public Task<T> LimitAsync<T>(Func<Task<T>> taskFactory)
    {
        foreach (var limiter in Limiters)
        {
            // Allocation is necessary otherwise it would point itself indefinitely
            var tmp = taskFactory;
            taskFactory = () => limiter.LimitAsync(tmp);
        }

        return taskFactory();
    }

    #region Equality Methods

    public bool Equals(CompositeTaskLimiter? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Limiters.Count == other.Limiters.Count && Limiters.All(other.Limiters.Contains);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CompositeTaskLimiter)obj);
    }

    public override int GetHashCode()
    {
        return Limiters.GetHashCode();
    }

    public static bool operator ==(CompositeTaskLimiter? left, CompositeTaskLimiter? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CompositeTaskLimiter? left, CompositeTaskLimiter? right)
    {
        return !Equals(left, right);
    }

    #endregion
}