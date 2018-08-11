namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_PROPERTY_ID enumeration
    /// https://msdn.microsoft.com/library/windows/hardware/ff566996
    /// </summary>
    public enum StoragePropertyId : uint
    {
        /// <summary>
        /// Indicates that the caller is querying for the device descriptor. <see cref="StorageDeviceDescriptor"/>
        /// </summary>
        StorageDeviceProperty = 0,
        /// <summary>
        /// Indicates that the caller is querying for the adapter descriptor. <see cref="StorageAdapterDescriptor"/>
        /// </summary>
        StorageAdapterProperty = 1,
        /// <summary>
        /// Indicates that the caller is querying for the device identifiers provided with the SCSI vital product data pages. Datais returned using the <see cref="StorageDeviceIdDescriptor"/> structure.
        /// </summary>
        StorageDeviceIdProperty = 2,
        /// <summary>
        /// Indicates that the caller is querying for the unique device identifiers. Data is returned using the <see cref="StorageDeviceUniqueIdentifier"/> structure.
        /// </summary>
        StorageDeviceUniqueIdProperty = 3,
        /// <summary>
        /// Indicates that the caller is querying for the write cache property. Data is returned using the <see cref="StorageWriteCacheProperty"/> structure.
        /// </summary>
        StorageDeviceWriteCacheProperty = 4,
        /// <summary>
        /// Reserved for System use.
        /// </summary>
        StorageMiniportProperty = 5,
        /// <summary>
        /// Indicates that the caller is querying for the access aligment descriptor. <see cref="StorageAccessAlignmentDescriptor"/>
        /// </summary>
        StorageAccessAlignmentProperty = 6,
        /// <summary>
        /// Indicates that the caller is querying for the seekpanalty descriptor <see cref="DeviceSeekPenaltyDescriptor"/>
        /// </summary>
        StorageDeviceSeekPenaltyProperty = 7,
        /// <summary>
        /// Indicates that the caller is querying for the trim descriptor, <see cref="DeviceTrimDescriptor"/>.
        /// </summary>
        StorageDeviceTrimProperty = 8,
        /// <summary>
        /// Reserved for system use.
        /// </summary>
        StorageDeviceWriteAggregationProperty = 9,
        /// <summary>
        /// Reserved for system use.
        /// </summary>
        StorageDeviceDeviceTelemetryProperty = 10,
        /// <summary>
        /// Indicates that the caller is querying for the logical block provisioning property. Data is returned using the <see cref="DeviceLbProvisioningDescriptor"/> structure.
        /// </summary>
        StorageDeviceLBProvisioningProperty = 11, // 0xB
        /// <summary>
        /// Indicates that the caller is querying for the device power descriptor. Data is returned using the <see cref="DevicePowerDescriptor"/> structure.
        /// </summary>
        StorageDevicePowerProperty = 12, // 0xC
        StorageDeviceCopyOffloadProperty = 13, // 0xD
        StorageDeviceResiliencyProperty = 14 // 0xE
    }
}