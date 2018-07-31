using System;
using System.Collections.Generic;

namespace IoControlPhysicalExample
{
    public static class DisposableExtensions
    {
        public static IEnumerable<T> Using<T>(this IEnumerable<T> self)
            where T : IDisposable
        {
            foreach (var n in self)
                using (n)
                    yield return n;
        }
    }
}
