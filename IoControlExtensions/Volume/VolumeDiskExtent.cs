using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct VolumeDiskExtent
    {
        public uint NumberOfDiskExtents;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public DiskExtent[] Extents;
        public override string ToString()
            => $"{nameof(VolumeDiskExtent)}{{{nameof(NumberOfDiskExtents)}:{NumberOfDiskExtents}, [{string.Join(" , ", (Extents ?? Enumerable.Empty<DiskExtent>()).Take((int)NumberOfDiskExtents).Select(v => $"{v}"))}]}}";
    }
}
