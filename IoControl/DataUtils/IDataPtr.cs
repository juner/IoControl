using System;

namespace IoControl.DataUtils { 
    public interface IDataPtr : IPtrCreatable
    {
        void SetPtr(IntPtr IntPtr, uint Size);
        object Get();
    }
    public interface IDataPtr<T> : IDataPtr
        where T : struct
    {
        new T Get();
    }
}
