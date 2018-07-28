using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DISK_GEOMETRY_EX structure (https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_disk_geometry_ex)
    /// </summary>
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
