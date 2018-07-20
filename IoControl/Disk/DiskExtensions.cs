using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace IoControl.Disk
{
    /// <summary>
    /// Disk Management Control Codes ( https://docs.microsoft.com/en-us/windows/desktop/fileio/disk-management-control-codes )
    /// </summary>
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
        public static void DiskPerformance(this IoControl IoControl, out DiskPerformance performance)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskPerformance, out performance, out _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
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
