namespace Aosta.Common.Limiter;

/// <summary>
/// A task limiter that throttles task executions using <see cref="SemaphoreSlim"/>
/// </summary>
internal class TaskLimiter : ITaskLimiter
{
    private readonly SemaphoreSlim _semaphore;

    /// <summary> Configuration of the task limiter </summary>
    internal TaskLimiterConfiguration Configuration { get; }

    /// <summary> Implicitly convert a <see cref="TaskLimiterConfiguration">Configuration</see> into its concrete <see cref="TaskLimiter"/> </summary>
    /// <param name="c"> The task limiter configuration </param>
    /// <returns> A new <see cref="TaskLimiter"/> built with that <see cref="TaskLimiterConfiguration">Configuration</see> </returns>
    public static implicit operator TaskLimiter(TaskLimiterConfiguration c) => new(c);

    internal TaskLimiter(TaskLimiterConfiguration config)
    {
        Configuration = config;

        _semaphore = new SemaphoreSlim(Configuration.Count, Configuration.Count);
    }

    /// <summary>
    /// Throttle the passed task execution according to the limiter <see cref="Configuration"/>
    /// </summary>
    /// <param name="taskFactory">Delegate that represents a method that returns a <see cref="Task"/></param>
    public async Task LimitAsync(Func<Task> taskFactory)
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);

        var task = taskFactory();

        // Fire and forget semaphore release
        _ = task.ContinueWith(async _ =>
        {
            await Task.Delay(Configuration.TimeSpan);
            _semaphore.Release(1);
        });

        await task;
    }

    /// <summary>
    /// Throttle the passed task execution according to the limiter <see cref="Configuration"/>
    /// </summary>
    /// <param name="taskFactory">Delegate that represents a method that returns a <see cref="Task{T}"/></param>
    /// <returns> The awaited <see cref="Task{T}"/> result </returns>
    public async Task<T> LimitAsync<T>(Func<Task<T>> taskFactory)
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);

        var task = taskFactory();

        // Fire and forget semaphore release
        _ = task.ContinueWith(async _ =>
        {
            await Task.Delay(Configuration.TimeSpan);
            _semaphore.Release(1);
        });

        return await task;
    }
}