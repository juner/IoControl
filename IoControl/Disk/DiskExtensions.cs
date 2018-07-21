using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IoControl.Disk
{
    public static class DiskExtensions
    {
        public static void DiskGetCacheInformation(this IoControl IoControl, out DiskCacheInformation information)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetCacheInformation, out information, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static DiskCacheInformation DiskGetCacheInformation(this IoControl IoControl)
        {
            DiskGetCacheInformation(IoControl, out var information);
            return information;
        }
        public static void DiskSetCacheInformation(this IoControl IoControl, ref DiskCacheInformation information)
        {
            var result = IoControl.DeviceIoControl(IOControlCode.DiskSetCacheInformation, ref information, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static void DiskGetDriveLayoutEx(this IoControl IoControl, out DriveLayoutInformationEx layout)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveLayoutEx, out layout, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static DriveLayoutInformationEx DiskGetDriveLayoutEx(this IoControl IoControl)
        {
            DiskGetDriveLayoutEx(IoControl, out var layout);
            return layout;
        }
        public static void DiskSetDriveLayoutEx(this IoControl IoControl, ref DriveLayoutInformationEx layout)
        {
            var result = IoControl.DeviceIoControl(IOControlCode.DiskSetDriveLayoutEx, ref layout, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static void DiskGetLengthInfo(this IoControl IoControl, out long Length)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetLengthInfo, out Length, out _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static long DiskGetLengthInfo(this IoControl IoControl)
        {
            DiskGetLengthInfo(IoControl, out var Length);
            return Length;
        }
        public static void DiskGetDriveGeometryEx(this IoControl IoControl, out DiskGeometryEx geometry)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveGeometryEx, out geometry, out _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static DiskGeometryEx DiskGetDriveGeometryEx(this IoControl IoControl)
        {
            DiskGetDriveGeometryEx(IoControl, out var geometry);
            return geometry;
        }
        /// <summary>
        /// IOCTL_DISK_PERFORMANCE ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_performance )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="performance"></param>
        public static void DiskPerformance(this IoControl IoControl, out DiskPerformance performance)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskPerformance, out performance, out var ReturnBytes);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_DISK_PERFORMANCE ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_performance )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DiskPerformance DiskPerformance(this IoControl IoControl)
        {
            DiskPerformance(IoControl, out var performance);
            return performance;
        }
    }
    #region DiskCacheInformation
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public struct DiskCacheInformation
    {
        [FieldOffset(0)]
        public bool ParametersSavable;
        [FieldOffset(1)]
        public bool ReadCacheEnabled;
        [FieldOffset(2)]
        public bool WriteCacheEnabled;
        [FieldOffset(4)]
        public DiskCacheRetentionPriority ReadRetentionPriority;
        [FieldOffset(8)]
        public DiskCacheRetentionPriority WriteRetentionPriority;
        [FieldOffset(12)]
        public ushort DisablePrefetchTransferLength;
        [FieldOffset(14)]
        public bool PrefetchScalar;
        [FieldOffset(16)]
        public DiskCacheInformationScalarPrefetch ScalarPrefetch;
        [FieldOffset(16)]
        public DiskCacheInformationBlockPrefetch BlockPrefetch;
        public override string ToString()
            => $"{nameof(DiskCacheInformation)}{{{nameof(ParametersSavable)}:{ParametersSavable}, {nameof(ReadCacheEnabled)}:{ReadCacheEnabled}, {nameof(WriteCacheEnabled)}:{WriteCacheEnabled}, {nameof(ReadRetentionPriority)}:{ReadRetentionPriority}, {nameof(WriteRetentionPriority)}:{WriteRetentionPriority}, {nameof(DisablePrefetchTransferLength)}:{DisablePrefetchTransferLength}, {nameof(PrefetchScalar)}:{PrefetchScalar}, {(PrefetchScalar ? $"{nameof(ScalarPrefetch)}:{ScalarPrefetch}" : $"{nameof(BlockPrefetch)}:{BlockPrefetch}")}}}";
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskCacheInformationScalarPrefetch
    {
        public ushort Minimum;
        public ushort Maximum;
        public ushort MaximumBlocks;
        public override string ToString()
            => $"{nameof(DiskCacheInformationScalarPrefetch)}{{{nameof(Minimum)}:{Minimum}, {nameof(Maximum)}:{Maximum}, {nameof(MaximumBlocks)}:{MaximumBlocks}}}";
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskCacheInformationBlockPrefetch
    {
        public ushort Minimum;
        public ushort Maximum;
        public override string ToString()
            => $"{nameof(DiskCacheInformationBlockPrefetch)}{{{nameof(Minimum)}:{Minimum}, {nameof(Maximum)}:{Maximum}}}";
    }
    public enum DiskCacheRetentionPriority : int
    {
        EqualPriority,
        KeepPrefetchedData,
        KeepReadData
    };
    #endregion
    #region DriveLayoutInformation
    public enum PartitionStyle : uint
    {
        Mbr = 0,
        Gpt = 1,
        Raw = 2
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct DriveLayoutInformationEx
    {
        public PartitionStyle PartitionStyle;

        public uint PartitionCount;

        public DriveLayoutInformationUnion DriveLayoutInformaition;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 0x16)]
        public PartitionInformationEx[] PartitionEntry;
        public override string ToString()
            => $"{nameof(DriveLayoutInformationEx)}{{ {nameof(PartitionStyle)}:{PartitionStyle}, {nameof(PartitionCount)}:{PartitionCount}, {nameof(DriveLayoutInformaition)}:{(PartitionStyle == PartitionStyle.Gpt ? DriveLayoutInformaition.Gpt.ToString() : PartitionStyle == PartitionStyle.Mbr ? DriveLayoutInformaition.Mbr.ToString() : null)}, {nameof(PartitionEntry)}:[{string.Join(", ", (PartitionEntry ?? Enumerable.Empty<PartitionInformationEx>()).Take((int)PartitionCount).Select(v => $"{v}"))}] }}";
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct DriveLayoutInformationUnion
    {
        [FieldOffset(0)]
        public DriveLayoutInformationMbr Mbr;

        [FieldOffset(0)]
        public DriveLayoutInformationGpt Gpt;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct DriveLayoutInformationMbr
    {
        public uint Signature;
        public uint CheckSum;
        public override string ToString()
            => $"{nameof(DriveLayoutInformationMbr)}{{{nameof(Signature)}:{Signature}, {nameof(CheckSum)}:{CheckSum}}}";
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct DriveLayoutInformationGpt
    {
        public Guid DiskId;
        public long StartingUsableOffset;
        public long UsableLength;
        public uint MaxPartitionCount;
        public override string ToString()
            => $"{nameof(DriveLayoutInformationGpt)}:{{{nameof(DiskId)}:{DiskId}, {nameof(StartingUsableOffset)}:{StartingUsableOffset}, {nameof(UsableLength)}:{UsableLength}, {nameof(MaxPartitionCount)}:{MaxPartitionCount}}}";
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct PartitionInformationEx
    {
        [MarshalAs(UnmanagedType.U4)]
        public PartitionStyle PartitionStyle;
        public long StartingOffset;
        public long PartitionLength;
        public uint PartitionNumber;
        public bool RewritePartition;
        public PartitionInformationUnion Info;
        public PartitionInformationMbr Mbr { get => Info.Mbr; set => Info.Mbr = value; }
        public PartitionInformationGpt Gpt { get => Info.Gpt; set => Info.Gpt = value; }
        public override string ToString()
        {
            return $"{nameof(PartitionInformationEx)}{{" +
                $" {nameof(PartitionStyle)}:{PartitionStyle}" +
                $", {nameof(StartingOffset)}:{StartingOffset}" +
                $", {nameof(PartitionLength)}:{PartitionLength}" +
                $", {nameof(PartitionNumber)}:{PartitionNumber}" +
                $", {nameof(RewritePartition)}:{RewritePartition}" +
                $", " + (
                    PartitionStyle == PartitionStyle.Gpt ? $"{nameof(Gpt)}:{Gpt}" :
                    PartitionStyle == PartitionStyle.Mbr ? $"{nameof(Mbr)}:{Mbr}" : "null") +
                $"}}";
        }
    }
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct PartitionInformationUnion
    {
        [FieldOffset(0)]
        public PartitionInformationGpt Gpt;
        [FieldOffset(0)]
        public PartitionInformationMbr Mbr;
    }
    [StructLayout(LayoutKind.Sequential, Size = 8, Pack = 1)]
    public struct PartitionInformationMbr
    {
        /// <summary>
        /// The type of partition. For a list of values, see Disk Partition Types.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public PartitionType PartitionType;

        /// <summary>
        /// If this member is TRUE, the partition is bootable.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool BootIndicator;

        /// <summary>
        /// If this member is TRUE, the partition is of a recognized type.
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool RecognizedPartition;

        /// <summary>
        /// The number of hidden sectors in the partition.
        /// </summary>
        public uint HiddenSectors;
        public override string ToString() => $"{nameof(PartitionInformationMbr)}{{" +
            $" {nameof(PartitionType)}:{PartitionType}" +
            $", {nameof(BootIndicator)}:{BootIndicator}" +
            $", {nameof(RecognizedPartition)}:{RecognizedPartition}" +
            $", {nameof(HiddenSectors)}:{HiddenSectors}" +
            $"}}";
    }


    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Pack = 4)]
    public struct PartitionInformationGpt
    {
        [FieldOffset(0)]
        public Guid PartitionType;
        [FieldOffset(16)]
        public Guid PartitionId;
        [FieldOffset(32)]
        [MarshalAs(UnmanagedType.U8)]
        public EFIPartitionAttributes Attributes;
        [FieldOffset(40)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public string Name;
        public override string ToString() => $"{nameof(PartitionInformationGpt)}:{{" +
            $" {nameof(PartitionType)}:{PartitionType}" +
            $", {nameof(PartitionId)}:{PartitionId}" +
            $", {nameof(Attributes)}:{Attributes}" +
            $", {nameof(Name)}:{Name}" +
            $"}}";
    }
    public enum PartitionType : byte
    {
        EntryUnused = 0x00, // Entry unused
        Fat12 = 0x01, // 12-bit FAT entries
        Xenix1 = 0x02, // Xenix
        Xenix2 = 0x03, // Xenix
        Fat16 = 0x04, // 16-bit FAT entries
        Extended = 0x05, // Extended partition entry
        Huge = 0x06, // Huge partition MS-DOS V4
        Ifs = 0x07, // IFS Partition
        OS2BOOTMGR = 0x0A, // OS/2 Boot Manager/OPUS/Coherent swap
        Fat32 = 0x0B, // FAT32
        Fat32Xint13 = 0x0C, // FAT32 using extended int13 services
        Xint13 = 0x0E, // Win95 partition using extended int13 services
        Xint13Extend = 0x0F, // Same as type 5 but uses extended int13 services
        Prep = 0x41, // PowerPC Reference Platform (PReP) Boot Partition
        Ldm = 0x42, // Logical Disk Manager partition
        Unix = 0x63, // Unix
        ValidNtft = 0xC0, // NTFT uses high order bits
        Ntft = 0x80,  // NTFT partition
        LinuxSwap = 0x82, //An ext2/ext3/ext4 swap partition
        LinuxNative = 0x83 //An ext2/ext3/ext4 native partition
    }
    [Flags]
    public enum EFIPartitionAttributes : ulong
    {
        GetAttributePlatformRequired = 0x0000_0000_0000_0001,
        LegacyBIOSBootale = 0x0000_0000_0000_0004,
        GptBasicDataAttributeNoDriveLetter = 0x8000_0000_0000_0000,
        GetBasicDataAttributeHidden = 0x4000_0000_0000_0000,
        GetBasicDataAttributeShadowCopy = 0x2000_0000_0000_0000,
        GetBasicDataAttributeReadOnly = 0x1000_0000_0000_0000,
    }
    #endregion
    #region DiskGeometry
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskGeometry
    {
        public long Cylinders;
        public MediaType MediaType;
        public uint TrackPerCylinder;
        public uint SectorsPerTrack;
        public uint BytesPerSector;
        public override string ToString()
            => $"{nameof(DiskGeometry)}{{{nameof(Cylinders)}:{Cylinders}, {nameof(MediaType)}:{MediaType}, {nameof(TrackPerCylinder)}:{TrackPerCylinder}, {nameof(SectorsPerTrack)}:{SectorsPerTrack}, {nameof(BytesPerSector)}:{BytesPerSector}}}";
    }
    public enum MediaType : uint
    {
        Unkowon = 0,
        RemovableMedia = 11,
        FixedMedia = 12,
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskGeometryEx
    {
        public DiskGeometry Geometry;
        public long DiskSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] Data;
        public override string ToString()
            => $"{nameof(DiskGeometryEx)}{{{nameof(Geometry)}:{Geometry}, {nameof(DiskSize)}:{DiskSize}, {nameof(Data)}:[{string.Join(" ", (Data ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";
    }
    #endregion
}
