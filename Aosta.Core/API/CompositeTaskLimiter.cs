namespace Aosta.Core;

internal class CompositeTaskLimiter : ITaskLimiter
{
    private readonly List<TaskLimiter> Limiters = new();

    internal CompositeTaskLimiter(IEnumerable<TaskLimiterConfiguration> limiterConfigurations)
    {
        foreach (var config in limiterConfigurations)
        {
           Limiters.Add(new TaskLimiter(config));
        }
    }

    public Task LimitAsync(Func<Task> taskFactory)
    {
        foreach (var limiter in Limiters)
        {
            var tmp = taskFactory;
            taskFactory = () => limiter.LimitAsync(tmp);
        }

        return taskFactory();
    }

    public Task<T> LimitAsync<T>(Func<Task<T>> taskFactory)
    {
        foreach (var limiter in Limiters)
        {
            var tmp = taskFactory;
            taskFactory = () => limiter.LimitAsync(tmp);
        }

        return taskFactory();
    }
}