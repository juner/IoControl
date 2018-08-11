using System.Runtime.InteropServices;
namespace IoControl.MassStorage
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct StorageDeviceIdDescriptor {
        public uint Version;
        public uint Size;
        public uint NumberOfIdentifiers;
        public byte[] Identifiers;
    }
}
