using System;

namespace IoControl.DataUtils { 
    public interface IDataPtr
    {
        IDisposable CreatePtr(out IntPtr IntPtr, out uint Size);
        void SetPtr(IntPtr IntPtr, uint Size);
        object Get();
    }
}
