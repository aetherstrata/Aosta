namespace Aosta.Core;

internal interface ITaskLimiter
{
    Task LimitAsync(Func<Task> taskFactory);

    Task<T> LimitAsync<T>(Func<Task<T>> taskFactory);
}