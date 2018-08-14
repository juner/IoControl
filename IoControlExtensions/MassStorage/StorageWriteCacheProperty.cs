using System;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_WRITE_CACHE_PROPERTY
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ns-winioctl-_storage_write_cache_property
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageWriteCacheProperty : IStorageDescriptor
    {
        uint IStorageDescriptor.Size => Size;
        uint IStorageDescriptor.Version => Version;
        /// <summary>
        /// Contains the size of this structure, in bytes. The value of this member will change as members are added to the structure.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// Specifies the total size of the data returned, in bytes. This may include data that follows this structure.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// A value from the WRITE_CACHE_TYPE enumeration that indicates the current write cache type.
        /// </summary>
        public readonly WriteCacheType WriteCacheType;
        /// <summary>
        /// A value from the WRITE_CACHE_ENABLE enumeration that indicates whether the write cache is enabled.
        /// </summary>
        public readonly WriteCacheEnable WriteCacheEnabled;
        /// <summary>
        /// A value from the WRITE_CACHE_CHANGE enumeration that indicates whether if the host can change the write cache characteristics.
        /// </summary>
        public readonly WriteCacheChange WriteCacheChangeable;
        /// <summary>
        /// A value from the WRITE_THROUGH enumeration that indicates whether the device supports write-through caching.
        /// </summary>
        public readonly WriteThrough WriteThroughSupported;
        /// <summary>
        /// A BOOLEAN value that indicates whether the device allows host software to flush the device cache. If TRUE, the device allows host software to flush the device cache. If FALSE, host software cannot flush the device cache.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool FlushCacheSupported;
        /// <summary>
        /// A BOOLEAN value that indicates whether a user can configure the device's power protection characteristics in the registry. If TRUE, a user can configure the device's power protection characteristics in the registry. If FALSE, the user cannot configure the device's power protection characteristics in the registry.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool UserDefinedPowerProtection;
        /// <summary>
        /// A BOOLEAN value that indicates whether the device has a battery backup for the write cache. If TRUE, the device has a battery backup for the write cache. If FALSE, the device does not have a battery backup for the writer cache.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool NVCacheEnabled;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WriteCacheType"></param>
        /// <param name="WriteCacheEnabled"></param>
        /// <param name="WriteCacheChangeable"></param>
        /// <param name="WriteThroughSupported"></param>
        /// <param name="FlushCacheSupported"></param>
        /// <param name="UserDefinedPowerProtection"></param>
        /// <param name="NVCacheEnabled"></param>
        public StorageWriteCacheProperty(WriteCacheType WriteCacheType, WriteCacheEnable WriteCacheEnabled, WriteCacheChange WriteCacheChangeable, WriteThrough WriteThroughSupported, bool FlushCacheSupported, bool UserDefinedPowerProtection, bool NVCacheEnabled)
            => (Version, Size, this.WriteCacheType, this.WriteCacheEnabled, this.WriteCacheChangeable, this.WriteThroughSupported, this.FlushCacheSupported, this.UserDefinedPowerProtection, this.NVCacheEnabled)
            = ((uint)Marshal.SizeOf<StorageWriteCacheProperty>(), (uint)Marshal.SizeOf<StorageWriteCacheProperty>(), WriteCacheType, WriteCacheEnabled, WriteCacheChangeable, WriteThroughSupported, FlushCacheSupported, UserDefinedPowerProtection, NVCacheEnabled);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        public StorageWriteCacheProperty(IntPtr IntPtr, uint Size) => this = (StorageWriteCacheProperty)Marshal.PtrToStructure(IntPtr, typeof(StorageWriteCacheProperty));
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(StorageWriteCacheProperty)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(WriteCacheType)}:{WriteCacheType}, {nameof(WriteCacheEnabled)}:{WriteCacheEnabled}, {nameof(WriteCacheChangeable)}:{WriteCacheChangeable}, {nameof(WriteThroughSupported)}:{WriteThroughSupported}, {nameof(FlushCacheSupported)}:{FlushCacheSupported}, {nameof(UserDefinedPowerProtection)}:{UserDefinedPowerProtection}, {nameof(NVCacheEnabled)}:{NVCacheEnabled}}}";
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