using System;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_DEVICE_UNIQUE_IDENTIFIER structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/storduid/ns-storduid-_storage_device_unique_identifier
    /// The <see cref="StorageDeviceUniqueIdentifier"/> structure defines a device unique identifier (DUID).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageDeviceUniqueIdentifier : IStorageDescriptor
    {
        uint IStorageDescriptor.Version => Version;
        uint IStorageDescriptor.Size => Size;
        /// <summary>
        /// The version of the DUID.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// The size, in bytes, of the identifier header and the identifiers (IDs) that follow the header.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// The offset, in bytes, from the beginning of the header to the device ID descriptor (<see cref="StorageDeviceIdDescriptor"/>). The device ID descriptor contains the IDs that are extracted from page 0x83 of the device's vital product data (VPD).
        /// </summary>
        public readonly uint StorageDeviceIdOffset;
        /// <summary>
        /// The offset, in bytes, from the beginning of the header to the device descriptor (<see cref="StorageDeviceDescriptor"/>). The device descriptor contains IDs that are extracted from non-VPD inquiry data.
        /// </summary>
        public readonly uint StorageDeviceOffset;
        /// <summary>
        /// The offset, in bytes, to the drive layout signature (<see cref="StorageDeviceLayoutSignature"/>).
        /// </summary>
        public readonly uint DriveLayoutSignatureOffset;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StorageDeviceIdOffset"></param>
        /// <param name="StorageDeviceOffset"></param>
        /// <param name="DriveLayoutSignatureOffset"></param>
        public StorageDeviceUniqueIdentifier(uint StorageDeviceIdOffset, uint StorageDeviceOffset, uint DriveLayoutSignatureOffset) => (Version, Size, this.StorageDeviceIdOffset, this.StorageDeviceOffset, this.DriveLayoutSignatureOffset)
                = ((uint)Marshal.SizeOf<StorageDeviceUniqueIdentifier>(), (uint)Marshal.SizeOf<StorageDeviceUniqueIdentifier>(), StorageDeviceIdOffset, StorageDeviceOffset, DriveLayoutSignatureOffset);
        /// <summary>
        /// The offset, in bytes, to the drive layout signature (<see cref="StorageDeviceLayoutSignature"/>).
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        public StorageDeviceUniqueIdentifier(IntPtr IntPtr, uint Size) => this = (StorageDeviceUniqueIdentifier)Marshal.PtrToStructure(IntPtr, typeof(StorageDeviceUniqueIdentifier));
        public override string ToString()
            => $"{nameof(StorageDeviceUniqueIdentifier)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(StorageDeviceIdOffset)}:{StorageDeviceIdOffset}, {nameof(StorageDeviceOffset)}:{StorageDeviceOffset}, {nameof(DriveLayoutSignatureOffset)}:{DriveLayoutSignatureOffset}}}";
    }
}