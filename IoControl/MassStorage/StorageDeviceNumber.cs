using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StorageDeviceNumber
    {
        public FileDevice DeviceType;
        public uint DeviceNumber;
        public uint PartitionNumber;
        public override string ToString()
            => $"{nameof(StorageDeviceNumber)}{{{nameof(DeviceType)}:{DeviceType}, {nameof(DeviceNumber)}:{DeviceNumber}, {nameof(PartitionNumber)}:{PartitionNumber}}}";
    }
}
