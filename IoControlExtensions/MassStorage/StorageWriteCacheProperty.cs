using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageWriteCacheProperty : IStorageDescriptor
    {
        uint IStorageDescriptor.Size => Size;
        uint IStorageDescriptor.Version => Version;
        public readonly uint Version;
        public readonly uint Size;
        public readonly WriteCacheType WriteCacheType;
        public readonly WriteCacheEnable WriteCAcheEnabled;
        public readonly WriteCacheChange WriteCacheChangeable;
        public readonly WriteThrough WriteThroughSupported;
        public readonly bool FlushCacheSupported;
        public readonly bool UserDfinedPowerProtection;
        public readonly bool NVCacheEnabled;
    }
    public enum WriteCacheType : int
    {
        Unknown = 0,
        None = 1,
        WriteBack = 2,
        WriteThrough = 3,
    }
    public enum WriteCacheEnable : int
    {
        Unknown = 0,
        Disabled = 1,
        Enabled = 2,
    }
    public enum WriteCacheChange : int
    {
        Unkown = 0,
        NotChangeable = 1,
        Changeable = 2,
    }
    public enum WriteThrough : int
    {
        Unkown = 0,
        NotSupported = 1,
        Supported = 2,
    }
}