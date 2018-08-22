using System;

namespace IoControl.DataUtils
{
    public class ExtendStructPtr<T> : DataPtr
        where T : struct
    {
        private T Struct;
        private Func<T, (IDisposable, IntPtr, uint)> StructToPtr = null;
        private Func<IntPtr, uint, T> PtrToStruct = null;
        public ExtendStructPtr(T Struct, Func<T, (IDisposable, IntPtr, uint)> StructToPtr, Func<IntPtr,uint, T> PtrToStruct) {
            if (StructToPtr == null)
                throw new ArgumentNullException(nameof(StructToPtr));
            if (PtrToStruct == null)
                throw new ArgumentNullException(nameof(PtrToStruct));
            (this.Struct, this.StructToPtr, this.PtrToStruct) = (Struct, StructToPtr, PtrToStruct);
        }
        public ref T Get() => ref Struct;
        object DataPtr.Get() => Get();

        public IDisposable GetPtrAndSize(out IntPtr IntPtr, out uint Size) {
            IDisposable Disposable = null;
            (Disposable, IntPtr, Size) = StructToPtr(Struct);
            return Disposable;
        }
        void DataPtr.SetPtr(IntPtr IntPtr, uint Size) => SetPtr(IntPtr, Size);
        public ref T SetPtr(IntPtr IntPtr, uint Size)
        {
            Struct = PtrToStruct(IntPtr, Size);
            return ref Struct;
        }
    }
}
