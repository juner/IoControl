using System;
using System.Runtime.InteropServices;

namespace IoControl.DataUtils
{
    /// <summary>
    /// byte ベースのデータ
    /// </summary>
    public class BytesPtr : IDataPtr
    {
        private readonly byte[] bytes;
        public BytesPtr(byte[] bytes) => this.bytes = bytes;
        public byte[] Get() => bytes;
        object IDataPtr.Get() => Get();

        public IDisposable CreatePtr(out IntPtr IntPtr, out uint Size)
        {
            var _Size = bytes.Length;
            var _IntPtr = Marshal.AllocCoTaskMem(_Size);
            var Disposable = global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(_IntPtr));
            try
            {
                Marshal.Copy(bytes, 0, _IntPtr, _Size);
                Size = (uint)_Size;
                IntPtr = _IntPtr;
            }
            catch
            {
                Disposable?.Dispose();
                throw;
            }
            return Disposable;
        }

        public void SetPtr(IntPtr IntPtr,uint Size) => Marshal.Copy(IntPtr, bytes, 0, bytes.Length);
    }
}
