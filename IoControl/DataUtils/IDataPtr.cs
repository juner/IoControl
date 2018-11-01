using System;

namespace IoControl.DataUtils { 
    public interface IDataPtr : IPtrCreatable
    {
        void SetPtr(IntPtr IntPtr, uint Size);
        object Get();
    }
}
