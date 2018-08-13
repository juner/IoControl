using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// _DEVICE_TRIM_DESCRIPTOR structure
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_device_trim_descriptor
    /// The <see cref="DeviceTrimDescriptor"/> structure is used in conjunction with the <see cref="IOControlCode.StorageQueryProperty"/> request to retrieve the trim descriptor data for a device.
    /// </summary>
    /// <remarks>
    /// Storage class drivers issue a device-control request with the I/O control code <see cref="IOControlCode.StorageQueryProperty"/> to retrieve this structure, which contains trim information for the device. The structure can be retrieved either from the device object for the bus or from an FDO, which forwards the request to the underlying bus.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceTrimDescriptor : IStorageDescriptor
    {
        uint IStorageDescriptor.Size => Size;
        uint IStorageDescriptor.Version => Version;
        /// <summary>
        /// Contains the size of the structure <see cref="DeviceTrimDescriptor"/>. The value of this member will change as members are added to the structure.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// Specifies the total size of the descriptor, in bytes.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// Specifies whether trim is enabled for the device.
        /// </summary>
        public readonly bool TrimEnabled;
    }
}
