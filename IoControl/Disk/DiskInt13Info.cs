using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DISK_INT113_INFO structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_disk_int13_info )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DiskInt13Info
    {
        /// <summary>
        /// Corresponds to the Device/Head register defined in the AT Attachment (ATA) specification. When zero, the fourth bit of this register indicates that drive zero is selected. When 1, it indicates that drive one is selected. The values of bits 0, 1, 2, 3, and 6 depend on the command in the command register. Bits 5 and 7 are no longer used. For more information about the values that the Device/Head register can hold, see the ATA specification.
        /// </summary>
        public readonly ushort DriveSelect;
        /// <summary>
        /// Indicates the maximum number of cylinders on the disk.
        /// </summary>
        public readonly uint MaxCylinders;
        /// <summary>
        /// Indicates the number of sectors per track.
        /// </summary>
        public readonly ushort SectorsPerTrack;
        /// <summary>
        /// Indicates the maximum number of disk heads.
        /// </summary>
        public readonly ushort MaxHeads;
        /// <summary>
        /// Indicates the number of drives.
        /// </summary>
        public readonly ushort NumberDrives;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DriveSelect"></param>
        /// <param name="MaxCylinders"></param>
        /// <param name="SectorsPerTrack"></param>
        /// <param name="MaxHeads"></param>
        /// <param name="NumberDrives"></param>
        public DiskInt13Info(ushort DriveSelect, uint MaxCylinders, ushort SectorsPerTrack, ushort MaxHeads, ushort NumberDrives)
            => (this.DriveSelect, this.MaxCylinders, this.SectorsPerTrack, this.MaxHeads, this.NumberDrives)
            = (DriveSelect, MaxCylinders, SectorsPerTrack, MaxHeads, NumberDrives);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DriveSelect"></param>
        /// <param name="MaxCylinders"></param>
        /// <param name="SectorsPerTrack"></param>
        /// <param name="MaxHeads"></param>
        /// <param name="NumberDrives"></param>
        public void Deconstruct(out ushort DriveSelect, out uint MaxCylinders, out ushort SectorsPerTrack, out ushort MaxHeads, out ushort NumberDrives)
            => (DriveSelect, MaxCylinders, SectorsPerTrack, MaxHeads, NumberDrives)
            = (this.DriveSelect, this.MaxCylinders, this.SectorsPerTrack, this.MaxHeads, this.NumberDrives);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(DiskInt13Info)}{{{nameof(DriveSelect)}:{DriveSelect}, {nameof(MaxCylinders)}:{MaxCylinders}, {nameof(SectorsPerTrack)}:{SectorsPerTrack}, {nameof(MaxHeads)}:{MaxHeads}, {nameof(NumberDrives)}:{NumberDrives}}}";
    }
}
