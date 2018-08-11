using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StorageDeviceUniqueIdentifier
    {
        public uint Version;
        public uint Size;
        public uint StorageDeviceIdOffset;
        public uint StorageDeviceOffset;
        public uint DriveLayoutSignatureOffset;
    }
}