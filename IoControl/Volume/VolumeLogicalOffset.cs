using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VolumeLogicalOffset
    {
        /// <summary>
        /// Specifies the logical location that needs to be translated to a physical location.
        /// </summary>
        public long LogicalOffset;
    }
}
