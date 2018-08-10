using System;
using System.Runtime.InteropServices;

namespace IoControl.DataPtr
{
    /// <summary>
    /// struct ベースの データ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StructPtr<T> : DataPtr
        where T : struct
    {
        protected T Struct;
        public StructPtr(T Struct) => this.Struct = Struct;
        public StructPtr() => Struct = default;
        public virtual IDisposable GetPtrAndSize(out IntPtr IntPtr, out uint Size)
        {
            var _Size = Marshal.SizeOf<T>();
            var _IntPtr = Marshal.AllocCoTaskMem(_Size);
            var Disposable = global::IoControl.Disposable.Create(() => Marshal.FreeCoTaskMem(_IntPtr));
            try
            {
                Marshal.StructureToPtr(this, _IntPtr, false);
            }
            catch
            {
                Disposable?.Dispose();
                throw;
            }
            IntPtr = _IntPtr;
            Size = (ushort)_Size;
            return Disposable; ;
        }
        void DataPtr.SetPtr(IntPtr IntPtr, uint Size) => SetPtr(IntPtr, Size);

        public virtual ref T SetPtr(IntPtr IntPtr, uint Size)
        {
            Struct = (T)Marshal.PtrToStructure(IntPtr, typeof(T));
            return ref Struct;
        }
        public ref T Get() => ref Struct;
        object DataPtr.Get() => Get();
    }
}
