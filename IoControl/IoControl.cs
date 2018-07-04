using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IoControl
{
    public class IoControl
    {
        public enum Method : uint
        {
            Buffered = 0,
            InDirect = 1,
            OutDirect = 2,
            Neither = 3
        }
        
        public enum FileDevice : uint
        {
            Beep = 0x00000001,
            CDRom = 0x00000002,
            CDRomFileSytem = 0x00000003,
            Controller = 0x00000004,
            Datalink = 0x00000005,
            Dfs = 0x00000006,
            Disk = 0x00000007,
            DiskFileSystem = 0x00000008,
            FileSystem = 0x00000009,
            InPortPort = 0x0000000a,
            Keyboard = 0x0000000b,
            Mailslot = 0x0000000c,
            MidiIn = 0x0000000d,
            MidiOut = 0x0000000e,
            Mouse = 0x0000000f,
            MultiUncProvider = 0x00000010,
            NamedPipe = 0x00000011,
            Network = 0x00000012,
            NetworkBrowser = 0x00000013,
            NetworkFileSystem = 0x00000014,
            Null = 0x00000015,
            ParallelPort = 0x00000016,
            PhysicalNetcard = 0x00000017,
            Printer = 0x00000018,
            Scanner = 0x00000019,
            SerialMousePort = 0x0000001a,
            SerialPort = 0x0000001b,
            Screen = 0x0000001c,
            Sound = 0x0000001d,
            Streams = 0x0000001e,
            Tape = 0x0000001f,
            TapeFileSystem = 0x00000020,
            Transport = 0x00000021,
            Unknown = 0x00000022,
            Video = 0x00000023,
            VirtualDisk = 0x00000024,
            WaveIn = 0x00000025,
            WaveOut = 0x00000026,
            Port8042 = 0x00000027,
            NetworkRedirector = 0x00000028,
            Battery = 0x00000029,
            BusExtender = 0x0000002a,
            Modem = 0x0000002b,
            Vdm = 0x0000002c,
            MassStorage = 0x0000002d,
            Smb = 0x0000002e,
            Ks = 0x0000002f,
            Changer = 0x00000030,
            Smartcard = 0x00000031,
            Acpi = 0x00000032,
            Dvd = 0x00000033,
            FullscreenVideo = 0x00000034,
            DfsFileSystem = 0x00000035,
            DfsVolume = 0x00000036,
            Serenum = 0x00000037,
            Termsrv = 0x00000038,
            Ksec = 0x00000039,
            // From Windows Driver Kit 7
            Fips = 0x0000003A,
            Infiniband = 0x0000003B,
            Vmbus = 0x0000003E,
            CryptProvider = 0x0000003F,
            Wpd = 0x00000040,
            Bluetooth = 0x00000041,
            MtComposite = 0x00000042,
            MtTransport = 0x00000043,
            Biometric = 0x00000044,
            Pmi = 0x00000045
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint CtrlCode(FileDevice DeviceType, uint Function, Method Method, FileAccess Access)
            => CtrlCode((uint)DeviceType, Function, (uint)Method, (uint)Access);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint CtrlCode(uint DeviceType, uint Function, uint Method, uint Access)
            => (DeviceType << 16) | (Function << 2) | Method | (Access << 14);

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
            VolumeGetVolumeDiskExtents = ('V' << 16) | (0 << 2) | Method.Buffered | (0 << 14),
            VolumeIsClustered = ('V' << 16) | (12 << 2) | Method.Buffered | (0 << 14),
        }
        class NativeMethod
        {
            [DllImport("kernel32.dll", SetLastError = true,
                CallingConvention = CallingConvention.StdCall,
                CharSet = CharSet.Auto)]
            public static extern SafeFileHandle CreateFile(
                 [MarshalAs(UnmanagedType.LPTStr)] string filename,
                 [MarshalAs(UnmanagedType.U4)] FileAccess access,
                 [MarshalAs(UnmanagedType.U4)] FileShare share,
                 IntPtr securityAttributes,
                 [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
                 [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
                 IntPtr templateFile);

            [DllImport("kernel32.dll", SetLastError = true,
                CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DeviceIoControl(
                SafeFileHandle hDevice,
                IOControlCode IoControlCode,
                IntPtr InBuffer,
                uint nInBufferSize,
                IntPtr OutBuffer,
                uint nOutBufferSize,
                out uint pBytesReturned,
                IntPtr Overlapped
            );
        }
        public static bool DeviceIoControl<TINOUT>(SafeFileHandle Device, IOControlCode IoControlCode, ref TINOUT InOutBuffer)
            where TINOUT : struct
            => DeviceIoControl(Device, IoControlCode, ref InOutBuffer, out var _, default);

        public static bool DeviceIoControl<TINOUT>(SafeFileHandle Device, IOControlCode IoControlCode, ref TINOUT InOutBuffer, out uint ReturnBytes, IntPtr pOverlapped = default)
            where TINOUT : struct
        {
            var inoutSize = (uint)Marshal.SizeOf(typeof(TINOUT));
            var _inoutBuffer = new byte[inoutSize];
            var iogch = GCHandle.Alloc(_inoutBuffer, GCHandleType.Pinned);
            using (Disposable.Create(iogch.Free))
            {
                var inoutPtr = iogch.AddrOfPinnedObject();
                Marshal.StructureToPtr(InOutBuffer, inoutPtr, false);
                var result = NativeMethod.DeviceIoControl(Device, IoControlCode, inoutPtr, inoutSize, inoutPtr, inoutSize, out ReturnBytes, pOverlapped);
                InOutBuffer = (TINOUT)Marshal.PtrToStructure(inoutPtr, typeof(TINOUT));
                return result;
            }
        }
        public static bool DeviceIoControl(SafeFileHandle Device, IOControlCode IoControlCode, byte[] InOutBuffer, out uint ReturnBytes, IntPtr pOverlapped = default)
        {
            var inoutSize = (uint)InOutBuffer.Length * sizeof(byte);
            var iogch = GCHandle.Alloc(InOutBuffer, GCHandleType.Pinned);
            using (Disposable.Create(iogch.Free))
            {
                var inoutPtr = iogch.AddrOfPinnedObject();
                var result = NativeMethod.DeviceIoControl(Device, IoControlCode, inoutPtr, inoutSize, inoutPtr, inoutSize, out ReturnBytes, pOverlapped);
                return result;
            }
        }
        public static bool DeviceIoControl<TIN, TOUT>(SafeFileHandle Device, IOControlCode IoControlCode, ref TIN InBuffer, out TOUT OutBuffer)
            where TIN : struct
            where TOUT : struct
            => DeviceIoControl(Device, IoControlCode, ref InBuffer, out OutBuffer, out var _);
        public static bool DeviceIoControl<TIN, TOUT>(SafeFileHandle Device, IOControlCode IoControlCode, ref TIN InBuffer, out TOUT OutBuffer, out uint ReturnBytes, IntPtr pOverlapped = default)
            where TIN : struct
            where TOUT : struct
        {
            var inSize = (uint)Marshal.SizeOf(typeof(TIN));
            var _inBuffer = new byte[inSize];
            var igch = GCHandle.Alloc(_inBuffer, GCHandleType.Pinned);
            var outSize = (uint)Marshal.SizeOf(typeof(TOUT));
            var _outBuffer = new byte[outSize];
            var ogch = GCHandle.Alloc(_outBuffer, GCHandleType.Pinned);
            using (Disposable.Create(igch.Free))
            using (Disposable.Create(ogch.Free))
            {
                var inPtr = igch.AddrOfPinnedObject();
                var outPtr = ogch.AddrOfPinnedObject();
                Marshal.StructureToPtr(InBuffer, inPtr, false);
                var result = NativeMethod.DeviceIoControl(Device, IoControlCode, inPtr, inSize, outPtr, outSize, out ReturnBytes, pOverlapped);
                InBuffer = (TIN)Marshal.PtrToStructure(inPtr, typeof(TIN));
                OutBuffer = (TOUT)Marshal.PtrToStructure(outPtr, typeof(TOUT));
                return result;
            }
        }
        public static bool DeviceIoControl(SafeFileHandle Device, IOControlCode dwIoControlCode, out uint ReturnBytes, IntPtr pOverlapped)
            => NativeMethod.DeviceIoControl(Device, dwIoControlCode, IntPtr.Zero, 0, IntPtr.Zero, 0, out ReturnBytes, pOverlapped);
        public static bool DeviceIoControlInOnly<TIN>(SafeFileHandle Device, IOControlCode dwIoControlCode, ref TIN InBuffer)
            where TIN : struct
            => DeviceIoControlInOnly(Device, dwIoControlCode, ref InBuffer, out var _);
        public static bool DeviceIoControlInOnly<TIN>(SafeFileHandle Device, IOControlCode dwIoControlCode, ref TIN InBuffer, out uint ReturnBytes, IntPtr pOverlapped = default)
            where TIN : struct
        {
            var inSize = (uint)Marshal.SizeOf(typeof(TIN));
            var _inBuffer = new byte[inSize];
            var igch = GCHandle.Alloc(_inBuffer, GCHandleType.Pinned);
            using (Disposable.Create(igch.Free))
            {
                var inPtr = igch.AddrOfPinnedObject();
                Marshal.StructureToPtr(InBuffer, inPtr, false);
                var result = NativeMethod.DeviceIoControl(Device, dwIoControlCode, inPtr, inSize, IntPtr.Zero, 0u, out ReturnBytes, pOverlapped);
                InBuffer = (TIN)Marshal.PtrToStructure(inPtr, typeof(TIN));
                return result;
            }
        }
        public static (bool Result, uint ReturnBytes) DeviceIoControlOutOnly(SafeFileHandle Device, IOControlCode IoControlCode, IntPtr OutPtr, uint OutSize, IntPtr Overlapped = default)
        {
            var result = NativeMethod.DeviceIoControl(Device, IoControlCode, IntPtr.Zero, 0, OutPtr, OutSize, out var ReturnBytes, Overlapped);
            return (result, ReturnBytes);
        }
        public static bool DeviceIoControlOutOnly<TOUT>(SafeFileHandle Device, IOControlCode IoControlCode, out TOUT OutBuffer, out uint ReturnBytes, IntPtr pOverlapped = default)
            where TOUT : struct
        {
            var outSize = (uint)Marshal.SizeOf(typeof(TOUT));
            var outPtr = Marshal.AllocCoTaskMem((int)outSize);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(outPtr)))
            {
                bool result;
                (result, ReturnBytes) = DeviceIoControlOutOnly(Device, IoControlCode, outPtr, outSize, pOverlapped);
                OutBuffer = (TOUT)Marshal.PtrToStructure(outPtr, typeof(TOUT));
                return result;
            }
        }

        public static bool DeviceIoControlOutOnly(SafeFileHandle Device, IOControlCode IoControlCode, byte[] OutBuffer, out uint ReturnBytes, IntPtr pOverlapped = default)
        {
            var outSize = (uint)OutBuffer.Length * sizeof(byte);
            var ogch = GCHandle.Alloc(OutBuffer, GCHandleType.Pinned);
            using (Disposable.Create(ogch.Free))
            {
                var outPtr = ogch.AddrOfPinnedObject();
                var result = NativeMethod.DeviceIoControl(Device, IoControlCode, IntPtr.Zero, 0, outPtr, outSize, out ReturnBytes, pOverlapped);
                return result;
            }
        }
        public static bool DeviceIoControl(SafeFileHandle hDevice, IOControlCode IoControlCode, IntPtr InBuffer, uint nInBufferSize, IntPtr OutBuffer, uint nOutBufferSize, out uint pBytesReturned, IntPtr Overlapped) => NativeMethod.DeviceIoControl(hDevice, IoControlCode, InBuffer, nInBufferSize, OutBuffer, nOutBufferSize, out pBytesReturned, Overlapped);
        internal class Disposable : IDisposable
        {
            Action Action;
            internal Disposable(Action Action) => this.Action = Action;
            public static Disposable Create(Action Action) => new Disposable(Action);
            public void Dispose()
            {
                try
                {
                    Action?.Invoke();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
                Action = null;
            }
        }
        public static SafeFileHandle CreateFile(string Filename, FileAccess FileAccess = default, FileShare FileShare = default, IntPtr SecurityAttributes = default, FileMode CreationDisposition = default, FileAttributes FlagsAndAttributes = default, IntPtr TemplateFile = default)
            => NativeMethod.CreateFile(Filename, FileAccess, FileShare, SecurityAttributes, CreationDisposition, FlagsAndAttributes, TemplateFile);
    }
}
