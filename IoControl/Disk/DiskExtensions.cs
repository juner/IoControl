using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IoControl.Disk
{
    /// <summary>
    /// Disk Management Control Codes ( https://docs.microsoft.com/en-us/windows/desktop/fileio/disk-management-control-codes )
    /// </summary>
    public static class DiskExtensions
    {
        /// <summary>
        /// IOCTL_DISK_ARE_VOLUMES_READY IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/fileio/ioctl-disk-are-volumes-ready )
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
        /// IOCTL_DISK_GET_LENGTH_INFO IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_length_info )
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
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_drive_geometry_ex )
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
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_drive_geometry_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DiskGeometryEx DiskGetDriveGeometryEx(this IoControl IoControl)
        {
            DiskGetDriveGeometryEx(IoControl, out var geometry);
            return geometry;
        }
        [Obsolete()]
        public static void DiskControllerNumber(this IoControl IoControl, out DiskControllerNumber number)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskControllerNumber, out number, out _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        [Obsolete()]
        public static DiskControllerNumber DiskControllerNumber(this IoControl IoControl)
        {
            DiskControllerNumber(IoControl, out var number);
            return number;
        }
        /// <summary>
        /// IOCTL_DISK_PERFORMANCE IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_performance )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="performance"></param>
        public static void DiskPerformance(this IoControl IoControl, out DiskPerformance performance)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskPerformance, out performance, out _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_DISK_PERFORMANCE IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_performance )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DiskPerformance DiskPerformance(this IoControl IoControl)
        {
            DiskPerformance(IoControl, out var performance);
            return performance;
        }
        public static void DiskPerformanceOff(this IoControl IoControl)
        {
            var result = IoControl.DeviceIoControl(IOControlCode.DiskPerformanceOff, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
    }
}
