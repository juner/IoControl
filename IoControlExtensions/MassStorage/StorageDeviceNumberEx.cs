using System;

namespace IoControl.MassStorage
{
    /// <summary>
    /// _STORAGE_DEVICE_NUMBER_EX
    /// </summary>
    public readonly struct StorageDeviceNumberEx
    {
        /// <summary>
        /// Sizeof(<see cref="StorageDeviceNumberEx"/>)
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// Total size of the structure, including any additional data. Currently this will always be the same as sizeof(<see cref="StorageDeviceNumberEx"/>).
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// Flags - this shall be a combination of STORAGE_DEVICE_FLAGS_XXX flags 
        /// that gives more information about the members of this structure.
        /// </summary>
        public readonly StorageDeviceFlags Flags;
        /// <summary>
        /// The FILE_DEVICE_XXX type for this device. This IOCTL is only supported for disk devices.
        /// </summary>
        public readonly FileDevice DeviceType;
        /// <summary>
        /// The number of this device.
        /// </summary>
        public readonly uint DeviceNumber;
        /// <summary>
        /// A globally-unique identification number for this device.
        /// A GUID of {0} indicates that a GUID could not be generated. The GUID
        /// is based on hardware information that doesn't change with firmware updates
        /// (for instance, serial number can be used to form the GUID, but not the firmware
        /// revision). The device GUID remains the same across reboots.
        ///
        /// In general, if a device exposes a globally unique identifier, the storage driver
        /// will use that identifier to form the GUID. Otherwise, the storage driver will combine
        /// the device's vendor ID, product ID and serial number to create the GUID.
        ///
        /// If a storage driver detects two devices with the same hardware information (which is
        /// an indication of a problem with the device), the driver will generate a random GUID for
        /// one of the two devices. When handling IOCTL_STORAGE_GET_DEVICE_NUMBER_EX for the device
        /// with the random GUID, the driver will add STORAGE_DEVICE_FLAGS_RANDOM_DEVICEGUID_REASON_CONFLICT
        /// to the Flags member of this structure.
        ///
        /// If a storage device does not provide any identifying information, the driver will generate a random
        /// GUID and add STORAGE_DEVICE_FLAGS_RANDOM_DEVICEGUID_REASON_NOHWID to the Flags member of this structure.
        /// A random GUID is not persisted and will not be the same after a reboot.
        /// </summary>
        public readonly Guid DeviceGuid;
        /// <summary>
        /// If the device is partitionable, the partition number of the device. Otherwise -1.
        /// </summary>
        public readonly uint PartitionNumber;
        public override string ToString()
            => $"{nameof(StorageDeviceNumberEx)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(Flags)}:{Flags}, {nameof(DeviceType)}:{DeviceType}, {nameof(DeviceNumber)}:{DeviceNumber}, {nameof(DeviceGuid)}:{DeviceGuid}, {nameof(PartitionNumber)}:{PartitionNumber}}}";
    }
}
