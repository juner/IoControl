using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IoControl.Disk
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/desktop/fileio/disk-management-control-codes
    /// </summary>
    public static class DiskExtensions
    {
        /// <summary>
        /// IOCTL_DISK_ARE_VOLUMES_READY control code ( https://docs.microsoft.com/en-us/windows/desktop/fileio/ioctl-disk-are-volumes-ready )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static Task DiskAreVolumesReadyAsync(this IoControl IoControl)  => throw new NotSupportedException();
        public static void CreateDisk(this IoControl IoControl, in CreateDisk CreateDisk)
        {

        }
        /// <summary>
        /// IOCTL_DISK_GET_CACHE_INFORMATION IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_cache_information )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="information"></param>
        public static void DiskGetCacheInformation(this IoControl IoControl, out DiskCacheInformation information)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetCacheInformation, out information, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_DISK_GET_CACHE_INFORMATION IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_cache_information )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
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
        /// <summary>
        /// IOCTL_DISK_GET_LENGTH_INFO ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_length_info )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static long DiskGetLengthInfo(this IoControl IoControl)
        {
            DiskGetLengthInfo(IoControl, out var Length);
            return Length;
        }
        /// <summary>
        /// 
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY_EX ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_drive_geometry_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="geometry"></param>
        public static void DiskGetDriveGeometryEx(this IoControl IoControl, out DiskGeometryEx geometry)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveGeometryEx, out geometry, out _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY_EX ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_drive_geometry_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DiskGeometryEx DiskGetDriveGeometryEx(this IoControl IoControl)
        {
            DiskGetDriveGeometryEx(IoControl, out var geometry);
            return geometry;
        }
        /// <summary>
        /// IOCTL_DISK_PERFORMANCE ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_performance )
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
        /// IOCTL_DISK_PERFORMANCE ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_performance )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DiskPerformance DiskPerformance(this IoControl IoControl)
        {
            DiskPerformance(IoControl, out var performance);
            return performance;
        }
    }
    /// <summary>
    /// CREATE_DISK structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_create_disk )
    /// </summary>
    public struct CreateDisk {
        public PartitionStyle PartitionStyle;
        public CreateDiskMbr Mbr;
        public CreateDiskGpt Gpt;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct CreateDiskMbr
    {
        public GUID DiskId;
        public uint MaxPartitionCount;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct CreateDiskGpt
    {
        public GUID DiskId;
        public uint MaxPartitionCount;
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
    public readonly struct GUID : IEquatable<GUID>, IEquatable<Guid>
    {
        public readonly int a;
        public readonly short b;
        public readonly short c;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public readonly byte[] d;
        public GUID(byte[] a) : this(BitConverter.ToInt32(a,0), BitConverter.ToInt16(a,4), BitConverter.ToInt16(a, 6), a[8], a[9], a[10], a[11], a[12], a[13], a[14], a[15]) { }
        public GUID(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k) :this(a,b,c,new byte[8] { d, e, f, g, h, i, j, k }) { }
        public GUID(int a,short b,short c, byte[] d) 
            => (this.a, this.b, this.c, this.d) = (a, b, c, d);
        public GUID(Guid Guid) : this(Guid.ToByteArray()) { }
        public static implicit operator Guid(in GUID GUID) => new Guid(GUID.a, GUID.b, GUID.c, GUID.d ?? new byte[8]);
        public static explicit operator byte[](in GUID GUID)
            => BitConverter.GetBytes(GUID.a)
            .Concat(BitConverter.GetBytes(GUID.b))
            .Concat(BitConverter.GetBytes(GUID.c))
            .Concat(GUID.d).ToArray();
        public bool Equals(GUID other) => ((byte[])this).SequenceEqual((byte[])other);
        public bool Equals(Guid other) => ((byte[])this).SequenceEqual(other.ToByteArray());
        public override string ToString()
            => $"{new Guid((byte[])this)}";
    }
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct PartitionInformationUnion
    {
        [FieldOffset(0)]
        public PartitionInformationGpt Gpt;
        [FieldOffset(0)]
        public PartitionInformationMbr Mbr;
    }
}
