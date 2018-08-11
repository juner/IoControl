using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct StorageHotplugInfo
    {
        /// <summary>
        /// version
        /// </summary>
        public uint Size;
        /// <summary>
        /// ie. zip, jaz, cdrom, mo, etc. vs hdd
        /// </summary>
        public bool MediaRemovable;
        /// <summary>
        /// ie. does the device succeed a lock even though its not lockable media?
        /// </summary>
        public bool MediaHotplug;
        /// <summary>
        /// ie. 1394, USB, etc.
        /// </summary>
        public bool DeviceHotplug;
        /// <summary>
        /// This field should not be relied upon because it is no longer used
        /// </summary>
        public bool WriteCacheEnableOverride;
    }
}
