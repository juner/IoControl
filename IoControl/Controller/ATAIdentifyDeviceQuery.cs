using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ATAIdentifyDeviceQuery
    {
        public AtaPassThroughEx Header;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public ushort[] Data;
        public override string ToString()
            => $"{nameof(ATAIdentifyDeviceQuery)}{{ {nameof(Header)}:{Header}, {nameof(Data)}:[{string.Join(" ", (Data ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}] }}";
    }
}
