using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VolumePhysicalOffsets
    {
        public uint NumberOfPhysicalOffsets;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public VolumePhysicalOffset[] PhysicalOffset;
    }
}
