using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    /// <summary>
    /// VOLUME_LOGICAL_OFFSET structure (https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddvol/ns-ntddvol-_volume_logical_offset)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VolumeLogicalOffset
    {
        /// <summary>
        /// Specifies the logical location that needs to be translated to a physical location.
        /// </summary>
        public long LogicalOffset;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogicalOffset"></param>
        public VolumeLogicalOffset(long LogicalOffset) => this.LogicalOffset = LogicalOffset;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Offset"></param>
        public static implicit operator long(in VolumeLogicalOffset Offset) => Offset.LogicalOffset;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogicalOffset"></param>
        public static implicit operator VolumeLogicalOffset(long LogicalOffset) => new VolumeLogicalOffset(LogicalOffset);
    }
}
