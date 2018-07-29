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
        /// The type of device. Values from 0 through 32,767 are reserved for use by Microsoft. Values from 32,768 through 65,535 are reserved for use by other vendors. The following values are defined by Microsoft:
        /// </summary>
        public FileDevice DeviceType;
        public uint DeviceNumber;
        public uint PartitionNumber;
        public override string ToString()
            => $"{nameof(StorageDeviceNumber)}{{{nameof(DeviceType)}:{DeviceType}, {nameof(DeviceNumber)}:{DeviceNumber}, {nameof(PartitionNumber)}:{PartitionNumber}}}";
    }
}
