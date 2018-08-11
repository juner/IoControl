using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct DiskExtent
    {
        public uint DiskNumber;
        public long StartingOffset;
        public long ExtentLength;
        public override string ToString()
            => $"{nameof(DiskExtent)}{{{nameof(DiskNumber)}:{DiskNumber}, {nameof(StartingOffset)}:{StartingOffset}, {nameof(ExtentLength)}:{ExtentLength}}}";
    }
}
