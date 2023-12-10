namespace Aosta.Common.Limiter
{
    /// <summary>
    /// The Throttler limits the invocation of an action to a specific time interval. <br/>
    /// This means that the action will only be executed once within the given time frame, regardless of how many times it is called.
    /// </summary>
    /// <typeparam name="T">Return Type of the executed tasks</typeparam>
    public class Throttler<T>
    {
        private readonly int _interval;
        private readonly bool _delayAfterExecution;
        private readonly bool _resetIntervalOnException;
        private readonly object _locker = new();
        private Task<T>? _lastTask;
        private DateTime? _invokeTime;
        private bool _busy;

        /// <summary>
        /// Throttler is a utility class for throttling the execution of asynchronous tasks.
        /// It limits the rate at which a function can be invoked based on a specified interval.
        /// </summary>
        /// <param name="interval">The minimum interval in milliseconds between invocations of the throttled function.</param>
        /// <param name="delayAfterExecution">If true, the interval is calculated from the end of the previous task execution, otherwise from the start.</param>
        /// <param name="resetIntervalOnException">If true, the interval is reset when an exception occurs during the execution of the throttled function.</param>
        public Throttler(int interval, bool delayAfterExecution = false, bool resetIntervalOnException = false)
        {
            _interval = interval;
            _delayAfterExecution = delayAfterExecution;
            _resetIntervalOnException = resetIntervalOnException;
        }

        private bool ShouldWait()
        {
            return _invokeTime.HasValue &&
                   (DateTime.UtcNow - _invokeTime.Value).TotalMilliseconds < _interval;
        }

        /// <summary>
        /// Throttling of the function invocation
        /// </summary>
        /// <param name="function">The function returns Task to be invoked asynchronously.</param>
        /// <param name="cancellationToken">An optional CancellationToken</param>
        /// <returns>Returns a last executed Task</returns>
        public Task<T> ThrottleAsync(Func<Task<T>> function, CancellationToken cancellationToken = default)
        {
            lock (_locker)
            {
                if (_lastTask != null && (_busy || ShouldWait()))
                {
                    return _lastTask;
                }

                _busy = true;
                _invokeTime = DateTime.UtcNow;

                _lastTask = function.Invoke();
                _lastTask.ContinueWith(_ =>
                {
                    if (_delayAfterExecution)
                    {
                        _invokeTime = DateTime.UtcNow;
                    }

                    _busy = false;
                }, cancellationToken);

                if (_resetIntervalOnException)
                {
                    _lastTask.ContinueWith((_, _) =>
                    {
                        _lastTask = null;
                        _invokeTime = null;
                    }, cancellationToken, TaskContinuationOptions.OnlyOnFaulted);
                }

                return _lastTask;
            }
        }
    }

    /// <summary>
    /// The Throttler limits the invocation of an action to a specific time interval. <br/>
    /// This means that the action will only be executed once within the given time frame, regardless of how many times it is called.
    /// </summary>
    public class Throttler : Throttler<bool>
    {
        public Throttler(int interval, bool delayAfterExecution = false, bool resetIntervalOnException = false)
            : base(interval, delayAfterExecution, resetIntervalOnException)
        {
        }

        /// <summary>
        /// ThrottleAsync method manages the throttling of the action invocation.
        /// </summary>
        /// <param name="function">The function returns the Task to be invoked asynchronously.</param>
        /// <param name="cancellationToken">An optional CancellationToken.</param>
        /// <returns></returns>
        public Task ThrottleAsync(Func<Task> function, CancellationToken cancellationToken = default)
        {
            return base.ThrottleAsync(async () =>
            {
                await function.Invoke();
                return true;
            }, cancellationToken);
        }

        /// <summary>
        /// Throttle method manages the throttling of the action invocation in a synchronous manner.
        /// </summary>
        /// <param name="action">The action to be invoked.</param>
        /// <param name="cancellationToken">An optional CancellationToken.</param>
        public void Throttle(Action action, CancellationToken cancellationToken = default)
        {
            Task<bool> ActionAsync() => Task.Run(() =>
            {
                action.Invoke();
                return true;
            }, cancellationToken);

            _ = ThrottleAsync(ActionAsync, cancellationToken);
        }
    }
}