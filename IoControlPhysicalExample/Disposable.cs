using System;

namespace IoControlPhysicalExample
{
    internal class Disposable : IDisposable
    {
        Action Action;
        internal Disposable(Action Action) => this.Action = Action;
        public static Disposable Create(Action Action) => new Disposable(Action);
        public void Dispose()
        {
            try
            {
                Action?.Invoke();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            Action = null;
        }
    }
}
