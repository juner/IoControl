using System.IO;

namespace IoControl
{
    /// <summary>
    /// IO Control Codes
    /// Useful links:
    ///     http://www.ioctls.net/
    ///     https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/defining-i-o-control-codes
    /// </summary>
    public enum IOControlCode : uint
    {
        // STORAGE
        StorageCheckVerify = (FileDevice.MassStorage << 16) | (0x0200 << 2) | Method.Buffered | (FileAccess.Read << 14),
        StorageCheckVerify2 = (FileDevice.MassStorage << 16) | (0x0200 << 2) | Method.Buffered | (0 << 14), // FileAccess.Any
        StorageMediaRemoval = (FileDevice.MassStorage << 16) | (0x0201 << 2) | Method.Buffered | (FileAccess.Read << 14),
        StorageEjectMedia = (FileDevice.MassStorage << 16) | (0x0202 << 2) | Method.Buffered | (FileAccess.Read << 14),
        StorageLoadMedia = (FileDevice.MassStorage << 16) | (0x0203 << 2) | Method.Buffered | (FileAccess.Read << 14),
        StorageLoadMedia2 = (FileDevice.MassStorage << 16) | (0x0203 << 2) | Method.Buffered | (0 << 14),
        StorageReserve = (FileDevice.MassStorage << 16) | (0x0204 << 2) | Method.Buffered | (FileAccess.Read << 14),
        StorageRelease = (FileDevice.MassStorage << 16) | (0x0205 << 2) | Method.Buffered | (FileAccess.Read << 14),
        StorageFindNewDevices = (FileDevice.MassStorage << 16) | (0x0206 << 2) | Method.Buffered | (FileAccess.Read << 14),
        StorageEjectionControl = (FileDevice.MassStorage << 16) | (0x0250 << 2) | Method.Buffered | (0 << 14),
        StorageMcnControl = (FileDevice.MassStorage << 16) | (0x0251 << 2) | Method.Buffered | (0 << 14),
        StorageGetMediaTypes = (FileDevice.MassStorage << 16) | (0x0300 << 2) | Method.Buffered | (0 << 14),
        StorageGetMediaTypesEx = (FileDevice.MassStorage << 16) | (0x0301 << 2) | Method.Buffered | (0 << 14),
        StorageResetBus = (FileDevice.MassStorage << 16) | (0x0400 << 2) | Method.Buffered | (FileAccess.Read << 14),
        StorageResetDevice = (FileDevice.MassStorage << 16) | (0x0401 << 2) | Method.Buffered | (FileAccess.Read << 14),
        StorageGetDeviceNumber = (FileDevice.MassStorage << 16) | (0x0420 << 2) | Method.Buffered | (0 << 14),
        StoragePredictFailure = (FileDevice.MassStorage << 16) | (0x0440 << 2) | Method.Buffered | (0 << 14),
        StorageObsoleteResetBus = (FileDevice.MassStorage << 16) | (0x0400 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        StorageObsoleteResetDevice = (FileDevice.MassStorage << 16) | (0x0401 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        StorageQueryProperty = (FileDevice.MassStorage << 16) | (0x0500 << 2) | Method.Buffered | (0 << 14),
        // DISK
        DiskGetDriveGeometry = (FileDevice.Disk << 16) | (0x0000 << 2) | Method.Buffered | (0 << 14),
        DiskGetDriveGeometryEx = (FileDevice.Disk << 16) | (0x0028 << 2) | Method.Buffered | (0 << 14),
        DiskGetPartitionInfo = (FileDevice.Disk << 16) | (0x0001 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskGetPartitionInfoEx = (FileDevice.Disk << 16) | (0x0012 << 2) | Method.Buffered | (0 << 14),
        DiskSetPartitionInfo = (FileDevice.Disk << 16) | (0x0002 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskGetDriveLayout = (FileDevice.Disk << 16) | (0x0003 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskSetDriveLayout = (FileDevice.Disk << 16) | (0x0004 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskVerify = (FileDevice.Disk << 16) | (0x0005 << 2) | Method.Buffered | (0 << 14),
        DiskFormatTracks = (FileDevice.Disk << 16) | (0x0006 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskReassignBlocks = (FileDevice.Disk << 16) | (0x0007 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskPerformance = (FileDevice.Disk << 16) | (0x0008 << 2) | Method.Buffered | (0 << 14),
        DiskIsWritable = (FileDevice.Disk << 16) | (0x0009 << 2) | Method.Buffered | (0 << 14),
        DiskLogging = (FileDevice.Disk << 16) | (0x000a << 2) | Method.Buffered | (0 << 14),
        DiskFormatTracksEx = (FileDevice.Disk << 16) | (0x000b << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskHistogramStructure = (FileDevice.Disk << 16) | (0x000c << 2) | Method.Buffered | (0 << 14),
        DiskHistogramData = (FileDevice.Disk << 16) | (0x000d << 2) | Method.Buffered | (0 << 14),
        DiskHistogramReset = (FileDevice.Disk << 16) | (0x000e << 2) | Method.Buffered | (0 << 14),
        DiskRequestStructure = (FileDevice.Disk << 16) | (0x000f << 2) | Method.Buffered | (0 << 14),
        DiskRequestData = (FileDevice.Disk << 16) | (0x0010 << 2) | Method.Buffered | (0 << 14),
        DiskControllerNumber = (FileDevice.Disk << 16) | (0x0011 << 2) | Method.Buffered | (0 << 14),
        DiskSmartGetVersion = (FileDevice.Disk << 16) | (0x0020 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskSmartSendDriveCommand = (FileDevice.Disk << 16) | (0x0021 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskSmartRcvDriveData = (FileDevice.Disk << 16) | (0x0022 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskUpdateDriveSize = (FileDevice.Disk << 16) | (0x0032 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskGrowPartition = (FileDevice.Disk << 16) | (0x0034 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskGetCacheInformation = (FileDevice.Disk << 16) | (0x0035 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskSetCacheInformation = (FileDevice.Disk << 16) | (0x0036 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskDeleteDriveLayout = (FileDevice.Disk << 16) | (0x0040 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskFormatDrive = (FileDevice.Disk << 16) | (0x00f3 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskSenseDevice = (FileDevice.Disk << 16) | (0x00f8 << 2) | Method.Buffered | (0 << 14),
        DiskCheckVerify = (FileDevice.Disk << 16) | (0x0200 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskMediaRemoval = (FileDevice.Disk << 16) | (0x0201 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskEjectMedia = (FileDevice.Disk << 16) | (0x0202 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskLoadMedia = (FileDevice.Disk << 16) | (0x0203 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskReserve = (FileDevice.Disk << 16) | (0x0204 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskRelease = (FileDevice.Disk << 16) | (0x0205 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskFindNewDevices = (FileDevice.Disk << 16) | (0x0206 << 2) | Method.Buffered | (FileAccess.Read << 14),
        DiskGetMediaTypes = (FileDevice.Disk << 16) | (0x0300 << 2) | Method.Buffered | (0 << 14),
        DiskSetPartitionInfoEx = (FileDevice.Disk << 16) | (0x0013 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskGetDriveLayoutEx = (FileDevice.Disk << 16) | (0x0014 << 2) | Method.Buffered | (0 << 14),
        DiskSetDriveLayoutEx = (FileDevice.Disk << 16) | (0x0015 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskCreateDisk = (FileDevice.Disk << 16) | (0x0016 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        DiskGetLengthInfo = (FileDevice.Disk << 16) | (0x0017 << 2) | Method.Buffered | (FileAccess.Read << 14),
        // CHANGER
        ChangerGetParameters = (FileDevice.Changer << 16) | (0x0000 << 2) | Method.Buffered | (FileAccess.Read << 14),
        ChangerGetStatus = (FileDevice.Changer << 16) | (0x0001 << 2) | Method.Buffered | (FileAccess.Read << 14),
        ChangerGetProductData = (FileDevice.Changer << 16) | (0x0002 << 2) | Method.Buffered | (FileAccess.Read << 14),
        ChangerSetAccess = (FileDevice.Changer << 16) | (0x0004 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        ChangerGetElementStatus = (FileDevice.Changer << 16) | (0x0005 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        ChangerInitializeElementStatus = (FileDevice.Changer << 16) | (0x0006 << 2) | Method.Buffered | (FileAccess.Read << 14),
        ChangerSetPosition = (FileDevice.Changer << 16) | (0x0007 << 2) | Method.Buffered | (FileAccess.Read << 14),
        ChangerExchangeMedium = (FileDevice.Changer << 16) | (0x0008 << 2) | Method.Buffered | (FileAccess.Read << 14),
        ChangerMoveMedium = (FileDevice.Changer << 16) | (0x0009 << 2) | Method.Buffered | (FileAccess.Read << 14),
        ChangerReinitializeTarget = (FileDevice.Changer << 16) | (0x000A << 2) | Method.Buffered | (FileAccess.Read << 14),
        ChangerQueryVolumeTags = (FileDevice.Changer << 16) | (0x000B << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        // FILESYSTEM
        FsctlRequestOplockLevel1 = (FileDevice.FileSystem << 16) | (0 << 2) | Method.Buffered | (0 << 14),
        FsctlRequestOplockLevel2 = (FileDevice.FileSystem << 16) | (1 << 2) | Method.Buffered | (0 << 14),
        FsctlRequestBatchOplock = (FileDevice.FileSystem << 16) | (2 << 2) | Method.Buffered | (0 << 14),
        FsctlOplockBreakAcknowledge = (FileDevice.FileSystem << 16) | (3 << 2) | Method.Buffered | (0 << 14),
        FsctlOpBatchAckClosePending = (FileDevice.FileSystem << 16) | (4 << 2) | Method.Buffered | (0 << 14),
        FsctlOplockBreakNotify = (FileDevice.FileSystem << 16) | (5 << 2) | Method.Buffered | (0 << 14),
        FsctlLockVolume = (FileDevice.FileSystem << 16) | (6 << 2) | Method.Buffered | (0 << 14),
        FsctlUnlockVolume = (FileDevice.FileSystem << 16) | (7 << 2) | Method.Buffered | (0 << 14),
        FsctlDismountVolume = (FileDevice.FileSystem << 16) | (8 << 2) | Method.Buffered | (0 << 14),
        FsctlIsVolumeMounted = (FileDevice.FileSystem << 16) | (10 << 2) | Method.Buffered | (0 << 14),
        FsctlIsPathnameValid = (FileDevice.FileSystem << 16) | (11 << 2) | Method.Buffered | (0 << 14),
        FsctlMarkVolumeDirty = (FileDevice.FileSystem << 16) | (12 << 2) | Method.Buffered | (0 << 14),
        FsctlQueryRetrievalPointers = (FileDevice.FileSystem << 16) | (14 << 2) | Method.Neither | (0 << 14),
        FsctlGetCompression = (FileDevice.FileSystem << 16) | (15 << 2) | Method.Buffered | (0 << 14),
        FsctlSetCompression = (FileDevice.FileSystem << 16) | (16 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        FsctlMarkAsSystemHive = (FileDevice.FileSystem << 16) | (19 << 2) | Method.Neither | (0 << 14),
        FsctlOplockBreakAckNo2 = (FileDevice.FileSystem << 16) | (20 << 2) | Method.Buffered | (0 << 14),
        FsctlInvalidateVolumes = (FileDevice.FileSystem << 16) | (21 << 2) | Method.Buffered | (0 << 14),
        FsctlQueryFatBpb = (FileDevice.FileSystem << 16) | (22 << 2) | Method.Buffered | (0 << 14),
        FsctlRequestFilterOplock = (FileDevice.FileSystem << 16) | (23 << 2) | Method.Buffered | (0 << 14),
        FsctlFileSystemGetStatistics = (FileDevice.FileSystem << 16) | (24 << 2) | Method.Buffered | (0 << 14),
        FsctlGetNtfsVolumeData = (FileDevice.FileSystem << 16) | (25 << 2) | Method.Buffered | (0 << 14),
        FsctlGetNtfsFileRecord = (FileDevice.FileSystem << 16) | (26 << 2) | Method.Buffered | (0 << 14),
        FsctlGetVolumeBitmap = (FileDevice.FileSystem << 16) | (27 << 2) | Method.Neither | (0 << 14),
        FsctlGetRetrievalPointers = (FileDevice.FileSystem << 16) | (28 << 2) | Method.Neither | (0 << 14),
        FsctlMoveFile = (FileDevice.FileSystem << 16) | (29 << 2) | Method.Buffered | (0 << 14),
        FsctlIsVolumeDirty = (FileDevice.FileSystem << 16) | (30 << 2) | Method.Buffered | (0 << 14),
        FsctlGetHfsInformation = (FileDevice.FileSystem << 16) | (31 << 2) | Method.Buffered | (0 << 14),
        FsctlAllowExtendedDasdIo = (FileDevice.FileSystem << 16) | (32 << 2) | Method.Neither | (0 << 14),
        FsctlReadPropertyData = (FileDevice.FileSystem << 16) | (33 << 2) | Method.Neither | (0 << 14),
        FsctlWritePropertyData = (FileDevice.FileSystem << 16) | (34 << 2) | Method.Neither | (0 << 14),
        FsctlFindFilesBySid = (FileDevice.FileSystem << 16) | (35 << 2) | Method.Neither | (0 << 14),
        FsctlDumpPropertyData = (FileDevice.FileSystem << 16) | (37 << 2) | Method.Neither | (0 << 14),
        FsctlSetObjectId = (FileDevice.FileSystem << 16) | (38 << 2) | Method.Buffered | (0 << 14),
        FsctlGetObjectId = (FileDevice.FileSystem << 16) | (39 << 2) | Method.Buffered | (0 << 14),
        FsctlDeleteObjectId = (FileDevice.FileSystem << 16) | (40 << 2) | Method.Buffered | (0 << 14),
        FsctlSetReparsePoint = (FileDevice.FileSystem << 16) | (41 << 2) | Method.Buffered | (0 << 14),
        FsctlGetReparsePoint = (FileDevice.FileSystem << 16) | (42 << 2) | Method.Buffered | (0 << 14),
        FsctlDeleteReparsePoint = (FileDevice.FileSystem << 16) | (43 << 2) | Method.Buffered | (0 << 14),
        FsctlEnumUsnData = (FileDevice.FileSystem << 16) | (44 << 2) | Method.Neither | (0 << 14),
        FsctlSecurityIdCheck = (FileDevice.FileSystem << 16) | (45 << 2) | Method.Neither | (FileAccess.Read << 14),
        FsctlReadUsnJournal = (FileDevice.FileSystem << 16) | (46 << 2) | Method.Neither | (0 << 14),
        FsctlSetObjectIdExtended = (FileDevice.FileSystem << 16) | (47 << 2) | Method.Buffered | (0 << 14),
        FsctlCreateOrGetObjectId = (FileDevice.FileSystem << 16) | (48 << 2) | Method.Buffered | (0 << 14),
        FsctlSetSparse = (FileDevice.FileSystem << 16) | (49 << 2) | Method.Buffered | (0 << 14),
        FsctlSetZeroData = (FileDevice.FileSystem << 16) | (50 << 2) | Method.Buffered | (FileAccess.Write << 14),
        FsctlQueryAllocatedRanges = (FileDevice.FileSystem << 16) | (51 << 2) | Method.Neither | (FileAccess.Read << 14),
        FsctlEnableUpgrade = (FileDevice.FileSystem << 16) | (52 << 2) | Method.Buffered | (FileAccess.Write << 14),
        FsctlSetEncryption = (FileDevice.FileSystem << 16) | (53 << 2) | Method.Neither | (0 << 14),
        FsctlEncryptionFsctlIo = (FileDevice.FileSystem << 16) | (54 << 2) | Method.Neither | (0 << 14),
        FsctlWriteRawEncrypted = (FileDevice.FileSystem << 16) | (55 << 2) | Method.Neither | (0 << 14),
        FsctlReadRawEncrypted = (FileDevice.FileSystem << 16) | (56 << 2) | Method.Neither | (0 << 14),
        FsctlCreateUsnJournal = (FileDevice.FileSystem << 16) | (57 << 2) | Method.Neither | (0 << 14),
        FsctlReadFileUsnData = (FileDevice.FileSystem << 16) | (58 << 2) | Method.Neither | (0 << 14),
        FsctlWriteUsnCloseRecord = (FileDevice.FileSystem << 16) | (59 << 2) | Method.Neither | (0 << 14),
        FsctlExtendVolume = (FileDevice.FileSystem << 16) | (60 << 2) | Method.Buffered | (0 << 14),
        FsctlQueryUsnJournal = (FileDevice.FileSystem << 16) | (61 << 2) | Method.Buffered | (0 << 14),
        FsctlDeleteUsnJournal = (FileDevice.FileSystem << 16) | (62 << 2) | Method.Buffered | (0 << 14),
        FsctlMarkHandle = (FileDevice.FileSystem << 16) | (63 << 2) | Method.Buffered | (0 << 14),
        FsctlSisCopyFile = (FileDevice.FileSystem << 16) | (64 << 2) | Method.Buffered | (0 << 14),
        FsctlSisLinkFiles = (FileDevice.FileSystem << 16) | (65 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        FsctlHsmMsg = (FileDevice.FileSystem << 16) | (66 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        FsctlNssControl = (FileDevice.FileSystem << 16) | (67 << 2) | Method.Buffered | (FileAccess.Write << 14),
        FsctlHsmData = (FileDevice.FileSystem << 16) | (68 << 2) | Method.Neither | (FileAccess.ReadWrite << 14),
        FsctlRecallFile = (FileDevice.FileSystem << 16) | (69 << 2) | Method.Neither | (0 << 14),
        FsctlNssRcontrol = (FileDevice.FileSystem << 16) | (70 << 2) | Method.Buffered | (FileAccess.Read << 14),
        // VIDEO
        VideoQuerySupportedBrightness = (FileDevice.Video << 16) | (0x0125 << 2) | Method.Buffered | (0 << 14),
        VideoQueryDisplayBrightness = (FileDevice.Video << 16) | (0x0126 << 2) | Method.Buffered | (0 << 14),
        VideoSetDisplayBrightness = (FileDevice.Video << 16) | (0x0127 << 2) | Method.Buffered | (0 << 14),

        // SCSI
        ScsiPassThrough = (FileDevice.Controller << 16) | (0x0401 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        ScsiMiniport = (FileDevice.Controller << 16) | (0x0402 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        ScsiGetInquiryData = (FileDevice.Controller << 16) | (0x0403 << 2) | Method.Buffered | (0 << 14),
        ScsiGetCapabilities = (FileDevice.Controller << 16) | (0x0404 << 2) | Method.Buffered | (0 << 14),
        ScsiPassThroughDirect = (FileDevice.Controller << 16) | (0x0405 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        ScsiGetAddress = (FileDevice.Controller << 16) | (0x0406 << 2) | Method.Buffered | (0 << 14),
        ScsiRescanBus = (FileDevice.Controller << 16) | (0x0407 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        ScsiGetDumpPointers = (FileDevice.Controller << 16) | (0x0408 << 2) | Method.Buffered | (0 << 14),
        ScsiFreeDumpPointers = (FileDevice.Controller << 16) | (0x0409 << 2) | Method.Buffered | (0 << 14),
        IdePassThrough = (FileDevice.Controller << 16) | (0x040a << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        AtaPassThrough = (FileDevice.Controller << 16) | (0x040b << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        AtaPassThroughDirect = (FileDevice.Controller << 16) | (0x040c << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),

        // These IOCTLs are handled by hard disk volumes.
        /// <summary>
        /// IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS
        /// </summary>
        VolumeGetVolumeDiskExtents = ('V' << 16) | (0 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_SUPPORTS_ONLINE_OFFLINE
        /// </summary>
        VolumeSupportsOnlineOffline = ('V' << 16) | (1 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_ONLINE 
        /// </summary>
        VolumeOnline = ('V' << 16) | (2 << 2) | Method.Buffered | (FileAccess.Write << 14),
        /// <summary>
        /// IOCTL_VOLUME_OFFLINE
        /// </summary>
        VolumeOffline = ('V' << 16) | (3 << 2) | Method.Buffered | (FileAccess.Write << 14),
        /// <summary>
        /// IOCTL_VOLUME_IS_OFFLINE
        /// </summary>
        VolumeIsOffline = ('V' << 16) | (4 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_IS_IO_CAPABLE
        /// </summary>
        VolumeIsIoCapable = ('V' << 16) | (5 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_QUERY_FAILOVER_SET
        /// </summary>
        VolumeQueryFailoverSet = ('V' << 16) | (6 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_QUERY_VOLUME_NUMBER
        /// </summary>
        VolumeQueryVolumeNumber = ('V' << 16) | (7 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_LOGICAL_TO_PHYSICAL
        /// </summary>
        VolumeLogicalToPhysical = ('V' << 16) | (8 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_PHYSICAL_TO_LOGICAL
        /// </summary>
        VolumePhysicalToLogical = ('V' << 16) | (9 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_IS_PARTITION
        /// </summary>
        VolumeIsPartition = ('V' << 16) | (10 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_READ_PLEX 
        /// </summary>
        VolumeReadPlex = ('V' << 16) | (11 << 2) | Method.Buffered | (FileAccess.Read << 14),
        /// <summary>
        /// IOCTL_VOLUME_IS_CLUSTERED 
        /// </summary>
        VolumeIsClustered = ('V' << 16) | (12 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_SET_GPT_ATTRIBUTES
        /// </summary>
        VolumeSetGptAttribute = ('V' << 16) | (13 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_GET_GPT_ATTRIBUTES
        /// </summary>
        VolumeGetGptAttribute = ('V' << 16) | (14 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_GET_BC_PROPERTIES
        /// </summary>
        VolumeGetBcProperties = ('V' << 16) | (15 << 2) | Method.Buffered | (FileAccess.Read << 14),
        /// <summary>
        /// IOCTL_VOLUME_ALLOCATE_BC_STREAM
        /// </summary>
        VolumeAllocateBcStream = ('V' << 16) | (16 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        /// <summary>
        /// IOCTL_VOLUME_FREE_BC_STREAM
        /// </summary>
        VolumeFreeBcStream = ('V' << 16) | (17 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        /// <summary>
        /// IOCTL_VOLUME_IS_DYNAMIC 
        /// </summary>
        VolumeIsDynamic = ('V' << 16) | (18 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_PREPARE_FOR_CRITICAL_IO
        /// </summary>
        VolumePrepareForCriticalIo = ('V' << 16) | (19 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        /// <summary>
        /// IOCTL_VOLUME_QUERY_ALLOCATION_HINT 
        /// </summary>
        VolumeQueryAllocationHint = ('V' << 16) | (20 << 2) | Method.Buffered | (FileAccess.Read << 14),
        /// <summary>
        /// IOCTL_VOLUME_UPDATE_PROPERTIES
        /// </summary>
        VolumeUpdateProperties = ('V' << 16) | (21 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_QUERY_MINIMUM_SHRINK_SIZE
        /// </summary>
        VolumeQueryMinimumShrinkSize = ('V' << 16) | (22 << 2) | Method.Buffered | (FileAccess.Read << 14),
        /// <summary>
        /// IOCTL_VOLUME_PREPARE_FOR_SHRINK
        /// </summary>
        VolumePrepareForShrink = ('V' << 16) | (23 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        /// <summary>
        /// IOCTL_VOLUME_IS_CSV
        /// </summary>
        VolumeIsCsv = ('V' << 16) | (24 << 2) | Method.Buffered | (0 << 14),
        /// <summary>
        /// IOCTL_VOLUME_POST_ONLINE
        /// </summary>
        VolumePostOnline = ('V' << 16) | (25 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
        /// <summary>
        /// IOCTL_VOLUME_GET_CSVBLOCKCACHE_CALLBACK CTL_CODE
        /// </summary>
        VolumeGetCsvblockcacheCallbackCtlCode = ('V' << 16) | (26 << 2) | Method.Buffered | (FileAccess.ReadWrite << 14),
    }
}
