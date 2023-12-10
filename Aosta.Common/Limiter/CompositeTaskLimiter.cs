namespace Aosta.Common.Limiter;

/// <summary>
/// A composite task limiter that throttles task executions by chaining task execution to its <see cref="limiters">TaskLimiters</see>.
/// The <see cref="Task"/> will execute when all <see cref="SemaphoreSlim"/> have a free slot.
/// <para/>
/// Concept example:
/// <code>limiter1.LimitAsync(() => limiter2.LimitAsync(() => ExpensiveMethod()))</code>
/// </summary>
public class CompositeTaskLimiter : ITaskLimiter
{
    /// <summary> List of <see cref="TaskLimiter">Task Limiters</see> </summary>
    private List<TaskLimiter> limiters { get; } = new();

    public CompositeTaskLimiter(IEnumerable<TaskLimiterConfiguration> limiterConfigs)
    {
        foreach (var configuration in limiterConfigs)
        {
            limiters.Add(new TaskLimiter(configuration));
        }
    }

    /// <summary>
    /// Throttle the passed task execution according to the component <see cref="limiters"/>
    /// </summary>
    /// <param name="taskFactory">Delegate that represents a method that returns a <see cref="Task"/></param>
    /// <returns> The input <see cref="Task"/> wrapped inside all <see cref="limiters"/></returns>
    public Task LimitAsync(Func<Task> taskFactory)
    {
        foreach (var limiter in limiters)
        {
            // Allocation is necessary otherwise it would point itself indefinitely
            var tmp = taskFactory;
            taskFactory = () => limiter.LimitAsync(tmp);
        }

        return taskFactory();
    }

    /// <summary>
    /// Throttle the passed task execution according to the component <see cref="limiters"/>
    /// </summary>
    /// <param name="taskFactory">Delegate that represents a method that returns a <see cref="Task{T}"/></param>
    /// <returns> The input <see cref="Task{T}"/> wrapped inside all <see cref="limiters"/></returns>
    public Task<T> LimitAsync<T>(Func<Task<T>> taskFactory)
    {
        foreach (var limiter in limiters)
        {
            // Allocation is necessary otherwise it would point itself indefinitely
            var tmp = taskFactory;
            taskFactory = () => limiter.LimitAsync(tmp);
        }

        return taskFactory();
    }
}
