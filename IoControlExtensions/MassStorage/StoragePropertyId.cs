using System;
using System.Linq;

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
        [StorageProperty(typeof(StorageDeviceDescriptor))]
        StorageDeviceProperty = 0,
        /// <summary>
        /// Indicates that the caller is querying for the adapter descriptor. <see cref="StorageAdapterDescriptor"/>
        /// </summary>
        [StorageProperty(typeof(StorageAdapterDescriptor))]
        StorageAdapterProperty,
        /// <summary>
        /// Indicates that the caller is querying for the device identifiers provided with the SCSI vital product data pages. Datais returned using the <see cref="StorageDeviceIdDescriptor"/> structure.
        /// </summary>
        [StorageProperty(typeof(StorageDeviceIdDescriptor))]
        StorageDeviceIdProperty,
        /// <summary>
        /// Indicates that the caller is querying for the unique device identifiers. Data is returned using the <see cref="StorageDeviceUniqueIdentifier"/> structure.
        /// </summary>
        [StorageProperty(typeof(StorageDeviceUniqueIdentifier))]
        StorageDeviceUniqueIdProperty,
        /// <summary>
        /// Indicates that the caller is querying for the write cache property. Data is returned using the <see cref="StorageWriteCacheProperty"/> structure.
        /// </summary>
        [StorageProperty(typeof(StorageWriteCacheProperty))]
        StorageDeviceWriteCacheProperty,
        /// <summary>
        /// Reserved for System use.
        /// </summary>
        StorageMiniportProperty,
        /// <summary>
        /// Indicates that the caller is querying for the access aligment descriptor. <see cref="StorageAccessAlignmentDescriptor"/>
        /// </summary>
        [StorageProperty(typeof(StorageAccessAlignmentDescriptor))]
        StorageAccessAlignmentProperty,
        /// <summary>
        /// Indicates that the caller is querying for the seekpanalty descriptor <see cref="DeviceSeekPenaltyDescriptor"/>
        /// </summary>
        [StorageProperty(typeof(DeviceSeekPenaltyDescriptor))]
        StorageDeviceSeekPenaltyProperty,
        /// <summary>
        /// Indicates that the caller is querying for the trim descriptor, <see cref="DeviceTrimDescriptor"/>.
        /// </summary>
        [StorageProperty(typeof(DeviceTrimDescriptor))]
        StorageDeviceTrimProperty,
        /// <summary>
        /// Reserved for system use.
        /// </summary>
        StorageDeviceWriteAggregationProperty,
        /// <summary>
        /// Reserved for system use.
        /// </summary>
        StorageDeviceDeviceTelemetryProperty,
        /// <summary>
        /// Indicates that the caller is querying for the logical block provisioning property. Data is returned using the <see cref="DeviceLbProvisioningDescriptor"/> structure.
        /// </summary>
        [StorageProperty(typeof(DeviceLbProvisioningDescriptor))]
        StorageDeviceLBProvisioningProperty,
        /// <summary>
        /// Indicates that the caller is querying for the device power descriptor. Data is returned using the <see cref="DevicePowerDescriptor"/> structure.
        /// </summary>
        [StorageProperty(typeof(DevicePowerDescriptor))]
        StorageDevicePowerProperty,
        StorageDeviceCopyOffloadProperty,
        StorageDeviceResiliencyProperty,
        StorageDeviceMediumProductType,
        StorageDeviceIoCapabilityProperty = 48,
        /// <summary>
        /// Indicates that the caller is querying for protocol-specific data from the adapter. Data is returned using the <see cref="StorageProtocolDataDescriptor"/> structure. See the remarks for more info.
        /// </summary>
        [StorageProperty(typeof(StorageProtocolDataDescriptor))]
        StorageAdapterProtocolSpecificProperty,
        /// <summary>
        /// Indicates that the caller is querying for protocol-specific data from the device. Data is returned using the <see cref="StorageProtocolDataDescriptor"/> structure. See the remarks for more info.
        /// </summary>
        [StorageProperty(typeof(StorageProtocolDataDescriptor))]
        StorageDeviceProtocolSpecificProperty,
        StorageAdapterTemperatureProperty,
        StorageDeviceTemperatureProperty,
        StorageAdapterPhysicalTopologyProperty,
        StorageDevicePhysicalTopologyProperty,
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        StorageDeviceAttributesProperty,
    }
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class StoragePropertyAttribute : Attribute
    {
        public Type DestType;
        public StoragePropertyAttribute(Type DestType)
        {
            if (!typeof(IStorageDescriptor).IsAssignableFrom(DestType))
                throw new ArgumentException($"{DestType} は {nameof(IStorageDescriptor)}の継承関係にありません。");
            this.DestType = DestType;
        }
    }
    public static class StoragePropertyAttributeExtension {
        public static Type GetDestType(this StoragePropertyId PropertyId) => PropertyId.GetType().GetField(PropertyId.ToString()).GetCustomAttributes(typeof(StoragePropertyAttribute), false).Cast<StoragePropertyAttribute>().FirstOrDefault()?.DestType;
    }
}