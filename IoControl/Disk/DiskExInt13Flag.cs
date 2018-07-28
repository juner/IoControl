using System;

namespace IoControl.Disk
{
    [Flags]
    public enum DiskExInt13Flag : ushort{
        /// <summary>
        /// DMA boundary errors are handled transparently.
        /// </summary>
        DMABoundaryErrorsAreHandledTransparently = 0b0000_0001,
        /// <summary>
        /// The geometry supplied in bytes 8-12 is valid.
        /// </summary>
        TheGeometrySuppliedInBytes8_12IsValid = 0b000000_0010,
        /// <summary>
        /// Device is removable.
        /// </summary>
        IsRemovable = 0b0000_0100,
        /// <summary>
        /// Device is change line support (bit 2 must be set).
        /// </summary>
        ChangeLineSupport = 0b0000_10000,
        /// <summary>
        /// Device is lockable (bit must be set).
        /// </summary>
        IsLockable = 0b0001_0000,
        /// <summary>
        /// Device geometry is set to maximum, no media is present (bit 2 must be set). This bit is turned off when media is present is a removable media device.
        /// </summary>
        DeviceGeometryIsSetToMaximumNoMediaIsPresent = 0b0010_0000,

    }
}
