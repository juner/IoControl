namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_DEVICE_FLAGS
    /// </summary>
    public enum StorageDeviceFlags : uint
    {
        /// <summary>
        /// STORAGE_DEVICE_FLAGS_RANDOM_DEVICEGUID_REASON_CONFLICT
        /// This flag indicates that deviceguid is randomly created because a deviceguid conflict was observed
        /// </summary>
        RandomDeviceguidReasonConflict = 0x1,
        /// <summary>
        /// STORAGE_DEVICE_FLAGS_RANDOM_DEVICEGUID_REASON_NOHWID
        ///  This flag indicates that deviceguid is randomly created because the HW ID was not available
        /// </summary>
        RandomDeviceguidReasonNohwid = 0x2,
        /// <summary>
        /// STORAGE_DEVICE_FLAGS_PAGE_83_DEVICEGUID
        /// This flag indicates that deviceguid is created from the scsi page83 data.
        /// If this flag is not set this implies it's created from serial number or is randomly generated.
        /// </summary>
        Page83Deviceguid = 0x4,
    }
}
