using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StorageDeviceUniqueIdentifier : IStorageDescriptor
    {
        uint IStorageDescriptor.Size => Size;
        uint IStorageDescriptor.Version => Version;
        public uint Version;
        public uint Size;
        public uint StorageDeviceIdOffset;
        public uint StorageDeviceOffset;
        public uint DriveLayoutSignatureOffset;
    }
}