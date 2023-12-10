namespace Aosta.Common.Limiter
{
    /// <summary>
    /// The Debounce dispatcher delays the invocation of an action until a predetermined interval has elapsed since the last call. <br/>
    /// This ensures that the action is only invoked once after the calls have stopped for the specified duration.
    /// </summary>
    /// <typeparam name="T">Type of the debouncing Task</typeparam>
    public class Debouncer<T>
    {
        private readonly object _locker = new();
        private readonly int _interval;

        private Task<T>? _waitingTask;
        private Func<Task<T>>? _funcToInvoke;
        private DateTime _lastInvokeTime;

        /// <summary>
        /// Debouncing the execution of asynchronous tasks.
        /// It ensures that a function is invoked only once within a specified interval, even if multiple invocations are requested.
        /// </summary>
        /// <param name="interval">The minimum interval in milliseconds between invocations of the debounced function.</param>
        public Debouncer(int interval)
        {
            _interval = interval;
        }

        /// <summary>
        /// DebounceAsync method manages the debouncing of the function invocation.
        /// </summary>
        /// <param name="function">The function returns Task to be invoked asynchronously</param>
        /// <param name="cancellationToken">An optional CancellationToken</param>
        /// <returns>Returns Task to be executed with minimal delay</returns>
        public Task<T> DebounceAsync(Func<Task<T>> function, CancellationToken cancellationToken = default)
        {
            lock (_locker)
            {
                _funcToInvoke = function;
                _lastInvokeTime = DateTime.UtcNow;

                if (_waitingTask != null)
                {
                    return _waitingTask;
                }

                _waitingTask = Task.Run(async () =>
                {
                    do
                    {
                        double delay = _interval - (DateTime.UtcNow - _lastInvokeTime).TotalMilliseconds;
                        await Task.Delay((int)(delay < 0 ? 0 : delay), cancellationToken);
                    }
                    while (DelayCondition());

                    T res;
                    try
                    {
                        res = await _funcToInvoke.Invoke();
                    }
                    finally
                    {
                        lock (_locker)
                        {
                            _waitingTask = null;
                        }
                    }
                    return res;

                }, cancellationToken);

                return _waitingTask;
            }
        }

        private bool DelayCondition()
        {
            return (DateTime.UtcNow - _lastInvokeTime).TotalMilliseconds < _interval;
        }
    }

    /// <summary>
    /// The Debounce dispatcher delays the invocation of an action until a predetermined interval has elapsed since the last call. <br/>
    /// This ensures that the action is only invoked once after the calls have stopped for the specified duration.
    /// </summary>
    public class Debouncer : Debouncer<bool>
    {
        /// <summary>
        /// Debouncing the execution of asynchronous tasks.
        /// It ensures that a function is invoked only once within a specified interval, even if multiple invocations are requested.
        /// </summary>
        /// <param name="interval">The minimum interval in milliseconds between invocations of the debounced function.</param>
        public Debouncer(int interval) : base(interval)
        {
        }

        /// <summary>
        /// Method manages the debouncing of the function invocation.
        /// </summary>
        /// <param name="function">The function returns Task to be invoked asynchronously</param>
        /// <param name="cancellationToken">An optional CancellationToken</param>
        /// <returns>Returns Task to be executed with minimal delay</returns>
        public Task DebounceAsync(Func<Task> function, CancellationToken cancellationToken = default)
        {
            return base.DebounceAsync(async () =>
            {
                await function.Invoke();
                return true;
            }, cancellationToken);
        }

        /// <summary>
        /// Method manages the debouncing of the function invocation.
        /// </summary>
        /// <param name="action">The action to be invoked</param>
        /// <param name="cancellationToken">An optional CancellationToken</param>
        public void Debounce(Action action, CancellationToken cancellationToken = default)
        {
            _ = DebounceAsync(ActionAsync, cancellationToken);

            Task<bool> ActionAsync() => Task.Run(() =>
            {
                action.Invoke();
                return true;
            }, cancellationToken);
        }
    }
}
