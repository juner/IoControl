using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DiskPartitionInfoMbr
    {
        public readonly uint Signature;
        public readonly uint CheckSum;
    }
}
