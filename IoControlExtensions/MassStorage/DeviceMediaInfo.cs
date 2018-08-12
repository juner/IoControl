using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// _DEVICE_MEDIA_INFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceMediaInfo
    {
        internal readonly DeviceMediaDeviceSpecific DeviceSpecific;
        public DeviceMediaDiskInfo DiskInfo => DeviceSpecific;
        public DeviceMediaRemovableDiskInfo RemovableDiskInfo => DeviceSpecific;
        public DeviceMediaTapInfo TapInfo => DeviceSpecific;
        public DeviceMediaInfo(DeviceMediaDiskInfo DiskInfo) => DeviceSpecific = DiskInfo;
        public DeviceMediaInfo(DeviceMediaRemovableDiskInfo RemovableDiskInfo) => DeviceSpecific = RemovableDiskInfo;
        public DeviceMediaInfo(DeviceMediaTapInfo TapInfo) => DeviceSpecific = TapInfo;
        [StructLayout(LayoutKind.Explicit)]
        internal readonly struct DeviceMediaDeviceSpecific
        {
            [FieldOffset(0)]
            public readonly DeviceMediaDiskInfo DiskInfo;
            [FieldOffset(0)]
            public readonly DeviceMediaRemovableDiskInfo RemovableDiskInfo;
            [FieldOffset(0)]
            public readonly DeviceMediaTapInfo TapInfo;
            public DeviceMediaDeviceSpecific(DeviceMediaDiskInfo DiskInfo) : this()
                => this.DiskInfo = DiskInfo;
            public DeviceMediaDeviceSpecific(DeviceMediaRemovableDiskInfo RemovableDiskInfo) : this()
                => this.RemovableDiskInfo = RemovableDiskInfo;
            public DeviceMediaDeviceSpecific(DeviceMediaTapInfo TapInfo) : this()
                => this.TapInfo = TapInfo;
            public static implicit operator DeviceMediaDiskInfo(in DeviceMediaDeviceSpecific DeviceSpecific) => DeviceSpecific.DiskInfo;
            public static implicit operator DeviceMediaRemovableDiskInfo(in DeviceMediaDeviceSpecific DeviceSpecific) => DeviceSpecific.RemovableDiskInfo;
            public static implicit operator DeviceMediaTapInfo(in DeviceMediaDeviceSpecific DeviceSpecific) => DeviceSpecific.TapInfo;
            public static implicit operator DeviceMediaDeviceSpecific(in DeviceMediaDiskInfo DiskInfo) => new DeviceMediaDeviceSpecific(DiskInfo);
            public static implicit operator DeviceMediaDeviceSpecific(in DeviceMediaRemovableDiskInfo RemovableDiskInfo) => new DeviceMediaDeviceSpecific(RemovableDiskInfo);
            public static implicit operator DeviceMediaDeviceSpecific(in DeviceMediaTapInfo TapInfo) => new DeviceMediaDeviceSpecific(TapInfo);
        }
    }
}
