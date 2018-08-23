using System.Runtime.InteropServices;

namespace IoControl.DataUtils
{
    public class AnySizeStruct<T> : StructPtr<T>, IAnySizeDataPtr
        where T : struct
    {
        public uint GetSize() => (uint)Marshal.SizeOf<T>();
    }
}
