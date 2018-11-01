using System;
using System.Runtime.InteropServices;
namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_DEVICE_NUMBER structure ( https://docs.microsoft.com/ja-jp/windows/desktop/api/winioctl/ns-winioctl-_storage_device_number )
    /// Contains information about a device. This structure is used by the <see cref="IOControlCode.StorageGetDeviceNumber"/> control code.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct StorageDeviceNumber
    {
        /// <summary>
        /// The FILE_DEVICE_XXX type for this device.
        /// </summary>
        public FileDevice DeviceType;
        /// <summary>
        /// The number of this device
        /// </summary>
        public uint DeviceNumber;
        /// <summary>
        /// If the device is partitionable, the partition number of the device.
        /// Otherwise -1
        /// </summary>
        public uint PartitionNumber;
        public bool IsNotHubPartitionNumber => PartitionNumber == uint.MaxValue;
        public override string ToString()
            => $"{nameof(StorageDeviceNumber)}{{{nameof(DeviceType)}:{DeviceType}, {nameof(DeviceNumber)}:{DeviceNumber}, {nameof(PartitionNumber)}:{PartitionNumber}}}";
        public static StorageDeviceNumber FromPtr(IntPtr IntPtr, uint Size) => PtrUtils.PtrToStructure<StorageDeviceNumber>(IntPtr, Size);
    }
}
