﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace IoControl
{
    /// <summary>
    /// http://www.thomaslevesque.com/2015/06/04/async-and-cancellation-support-for-wait-handles/
    /// </summary>
    internal static class WaitHandleExtensions {
        public static async Task<bool> WaitOneAsync(this WaitHandle handle, int millisecondsTimeout = Timeout.Infinite, CancellationToken cancellationToken = default)
        {
            RegisteredWaitHandle registeredHandle = null;
            CancellationTokenRegistration tokenRegistration = default;
            try
            {
                var tcs = new TaskCompletionSource<bool>();
                registeredHandle = ThreadPool.RegisterWaitForSingleObject(
                    handle,
                    (state, timedOut) => ((TaskCompletionSource<bool>)state).TrySetResult(!timedOut),
                    tcs,
                    millisecondsTimeout,
                    true);
                tokenRegistration = cancellationToken.Register(
                    state => ((TaskCompletionSource<bool>)state).TrySetCanceled(),
                    tcs);
                return await tcs.Task;
            }
            finally
            {
                if (registeredHandle != null)
                    registeredHandle.Unregister(null);
                tokenRegistration.Dispose();
            }
        }

        public static Task<bool> WaitOneAsync(this WaitHandle handle, TimeSpan timeout, CancellationToken cancellationToken) => handle.WaitOneAsync((int)timeout.TotalMilliseconds, cancellationToken);

        public static Task<bool> WaitOneAsync(this WaitHandle handle, CancellationToken cancellationToken) => handle.WaitOneAsync(Timeout.Infinite, cancellationToken);
    }
}
