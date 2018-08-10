using System;

namespace IoControl.DataPtr { 
    public interface DataPtr
    {
        IDisposable GetPtrAndSize(out IntPtr IntPtr, out uint Size);
        void SetPtr(IntPtr IntPtr, uint Size);
        object Get();
    }
}
