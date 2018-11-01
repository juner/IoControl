using System;

namespace IoControl.DataUtils
{
    /// <summary>
    /// <see cref="IntPtr"/>への変換を独自に実装している場合
    /// </summary>
    public interface IPtrCreatable
    {
        IDisposable CreatePtr(out IntPtr IntPtr, out uint Size);
    }
}
