using System.Runtime.InteropServices;

namespace IoControl.Volume
{
    /// <summary>
    /// VOLUME_PHYSICAL_OFFSET structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddvol/ns-ntddvol-_volume_physical_offset )
    /// The <see cref="VolumePhysicalOffset"/> structure contains a physical offset into a volume and its accompanying physical disk number and is used with <see cref="IOControlCode.VolumePhysicalToLogical"/> and <see cref="IOControlCode.VolumeLogicalToPhysical"/> to request a logical offset equivalent of a physical offset or a physical offset equivalent of a logical offset, respectively.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct VolumePhysicalOffset
    {
        /// <summary>
        /// Contains the number of the physical disk.
        /// </summary>
        public readonly uint DiskNumber;
        /// <summary>
        /// Contains the physical offset in bytes of the data on the disk.
        /// </summary>
        public readonly long Offset;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DiskNumber"></param>
        /// <param name="Offset"></param>
        public VolumePhysicalOffset(uint DiskNumber, long Offset)
            => (this.DiskNumber, this.Offset) = (DiskNumber, Offset);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DiskNumber"></param>
        /// <param name="Offset"></param>
        public void Deconstruct(out uint DiskNumber, out long Offset)
            => (DiskNumber, Offset) = (this.DiskNumber, this.Offset);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(VolumePhysicalOffset)}{{{nameof(DiskNumber)}:{DiskNumber}, {nameof(Offset)}:{Offset}}}";
    }
}
