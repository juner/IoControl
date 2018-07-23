using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VolumePhysicalOffset
    {
        /// <summary>
        /// Specifies the physical location that needs to be  translated to a logical location.
        /// </summary>
        public uint DiskNumber;

        public long Offset;
    }
}
