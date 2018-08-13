using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// DEVICE_POWER_DESCRIPTOR structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_device_power_descriptor
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DevicePowerDescriptor : IStorageDescriptor
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
        /// True if device attention is supported. Otherwise, <see cref="false"/>.
        /// </summary>
        public readonly bool DeviceAttentionSupported;
        /// <summary>
        /// True if the device supports asynchronous notifications, delivered via <see cref="IOControlCode.StorageEvent"/>. Otherwise, False
        /// </summary>
        public readonly bool AsynchronousNotificationSupported;
        public readonly bool IdlePowerManagementEnabled;
        public readonly bool D3ColdEnabled;
        public readonly bool D3ColdSupported;
        public readonly bool NoVerifyDuringIdlePower;
        public readonly byte[] Reserved;
        public readonly uint IdleTimeoutInMS;
    }
}
