namespace Aosta.Common.Limiter;

/// <summary>
/// Base task limiter implementation
/// </summary>
public interface ITaskLimiter
{
    Task LimitAsync(Func<Task> taskFactory);

    Task<T> LimitAsync<T>(Func<Task<T>> taskFactory);
}