using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VolumeFailoverSet
    {
        public uint NumberOfDisks;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =1)]
        public uint[] DiskNumbers;
        public override string ToString()
            => $"{nameof(VolumeFailoverSet)}{{{nameof(NumberOfDisks)}:{NumberOfDisks}, {nameof(DiskNumbers)}:[{string.Join(", ",(DiskNumbers ?? Enumerable.Empty<uint>()).Select(num => $"{num}"))}]}}";
    }
}
