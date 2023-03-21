using System.Diagnostics;

namespace Aosta.Core;

internal class TaskLimiter : ITaskLimiter, IEquatable<TaskLimiter>, IComparable<TaskLimiter>, IComparable
{
    private readonly SemaphoreSlim _semaphore;

    internal TaskLimiterConfiguration Configuration { get; }

    internal TaskLimiter(TaskLimiterConfiguration config)
    {
        Configuration = config;

        _semaphore = new SemaphoreSlim(Configuration.Count, Configuration.Count);
    }

    public async Task LimitAsync(Func<Task> taskFactory)
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);

        var task = taskFactory();

        // Fire and forget semaphore release
        _ = task.ContinueWith(async e =>
        {
            await Task.Delay(Configuration.TimeSpan);
            _semaphore.Release(1);
        });

        await task;
    }

    public async Task<T> LimitAsync<T>(Func<Task<T>> taskFactory)
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);

        var task = taskFactory();

        // Fire and forget semaphore release
        _ = task.ContinueWith(async e =>
        {
            await Task.Delay(Configuration.TimeSpan);
            _semaphore.Release(1);
        });

        return await task;
    }

    #region Equality methods

    public bool Equals(TaskLimiter? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Configuration.Equals(other.Configuration);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TaskLimiter)obj);
    }

    public override int GetHashCode()
    {
        return Configuration.GetHashCode();
    }

    public static bool operator ==(TaskLimiter? left, TaskLimiter? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TaskLimiter? left, TaskLimiter? right)
    {
        return !Equals(left, right);
    }

    #endregion

    #region Comparer methods

    public int CompareTo(TaskLimiter? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Configuration.CompareTo(other.Configuration);
    }

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is TaskLimiter other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(TaskLimiter)}");
    }

    public static bool operator <(TaskLimiter? left, TaskLimiter? right)
    {
        return Comparer<TaskLimiter>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(TaskLimiter? left, TaskLimiter? right)
    {
        return Comparer<TaskLimiter>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(TaskLimiter? left, TaskLimiter? right)
    {
        return Comparer<TaskLimiter>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(TaskLimiter? left, TaskLimiter? right)
    {
        return Comparer<TaskLimiter>.Default.Compare(left, right) >= 0;
    }

    #endregion
}