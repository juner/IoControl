using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskGeometryEx
    {
        public DiskGeometry Geometry;
        public long DiskSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] Data;
        public override string ToString()
            => $"{nameof(DiskGeometryEx)}{{{nameof(Geometry)}:{Geometry}, {nameof(DiskSize)}:{DiskSize}, {nameof(Data)}:[{string.Join(" ", (Data ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";
    }
}
