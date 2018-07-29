using System;
using System.Collections.Generic;
using System.Linq;
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
        const int ERROR_INSUFFICIENT_BUFFER = unchecked((int)0x8007007A);
        /// <summary>
        /// IOCTL_DISK_ARE_VOLUMES_READY IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/fileio/ioctl-disk-are-volumes-ready )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static Task DiskAreVolumesReadyAsync(this IoControl IoControl) =>  throw new NotImplementedException();
        public static void CreateDisk(this IoControl IoControl, in CreateDisk CreateDisk) => throw new NotImplementedException();
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
        /// <summary>
        /// IOCTL_DISK_GET_CACHE_INFORMATION IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_cache_information )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static void DiskSetCacheInformation(this IoControl IoControl, ref DiskCacheInformation information)
        {
            var result = IoControl.DeviceIoControl(IOControlCode.DiskSetCacheInformation, ref information, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static void DiskGetPartitionInfo(this IoControl IoControl, out PartitionInformation partition)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetPartitionInfo, out partition, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static PartitionInformation DiskGetPartitionInfo(this IoControl IoControl)
        {
            DiskGetPartitionInfo(IoControl, out var partition);
            return partition;
        }
        public static void DiskSetPartitionInfo(this IoControl IoControl, in PartitionInformation partition)
        {
            var result = IoControl.DeviceIoControlInOnly(IOControlCode.DiskSetPartitionInfo, partition, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static void DiskGetPartitionInfoEx(this IoControl IoControl, out PartitionInformationEx partition)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetPartitionInfoEx, out partition, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static PartitionInformationEx DiskGetPartitionInfoEx(this IoControl IoControl)
        {
            DiskGetPartitionInfoEx(IoControl, out var partition);
            return partition;
        }
        public static void DiskSetPartitionEx(this IoControl IoControl, in PartitionInformationEx partition)
        {
            var result = IoControl.DeviceIoControlInOnly(IOControlCode.DiskSetPartitionInfoEx, partition, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());

        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_get_drive_layout )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static void DiskGetDriveLayout(this IoControl IoControl, out DriveLayoutInformation layout)
        {
            var Size = (uint)Marshal.SizeOf<DriveLayoutInformation>();
            var ReturnSize = 0u;
            while(ReturnSize == 0u)
            {
                var Ptr = Marshal.AllocCoTaskMem((int)Size);
                using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
                {
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveLayout, Ptr, Size, out ReturnSize);
                    if (result)
                    {
                        layout = (DriveLayoutInformation)Marshal.PtrToStructure(Ptr, typeof(DriveLayoutInformation));
                        var ArrayPtr = IntPtr.Add(Ptr, Marshal.OffsetOf<DriveLayoutInformation>(nameof(layout._PartitionEntry)).ToInt32());
                        var PartitionSize = Marshal.SizeOf<PartitionInformation>();
                        layout = layout.Set(PartitionEntry: Enumerable
                            .Range(0, (int)layout.PartitionCount)
                            .Select(index => (PartitionInformation)Marshal.PtrToStructure(IntPtr.Add(ArrayPtr, PartitionSize * index), typeof(PartitionInformation)))
                            .ToArray());
                        return;
                    }
                    var ErrorCode = Marshal.GetHRForLastWin32Error();
                    if (ErrorCode == ERROR_INSUFFICIENT_BUFFER)
                    {
                        Size *= 2;
                        continue;
                    }
                    else
                    {
                        Marshal.ThrowExceptionForHR(ErrorCode);
                    }
                }
            }
            layout = default;
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_get_drive_layout )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static DriveLayoutInformation DiskGetDriveLayout(this IoControl IoControl)
        {
            DiskGetDriveLayout(IoControl, out var layout);
            return layout;
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_disk_get_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static void DiskGetDriveLayoutEx(this IoControl IoControl, out DriveLayoutInformationEx layout)
        {
            var Size = (uint)Marshal.SizeOf<DriveLayoutInformationEx>();
            var ReturnSize = 0u;
            while (ReturnSize == 0u)
            {
                var Ptr = Marshal.AllocCoTaskMem((int)Size);
                using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
                {
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveLayoutEx, Ptr, Size, out ReturnSize);
                    if (result)
                    {
                        layout = (DriveLayoutInformationEx)Marshal.PtrToStructure(Ptr, typeof(DriveLayoutInformationEx));
                        var offset = (int)Marshal.OffsetOf<DriveLayoutInformationEx>(nameof(layout._PartitionEntry));
                        var ArrayPtr = IntPtr.Add(Ptr, offset);
                        var PartitionSize = Marshal.SizeOf<PartitionInformationEx>();
                        layout = layout.Set(PartitionEntry: Enumerable
                            .Range(0, (int)layout.PartitionCount)
                            .Select(index => (PartitionInformationEx)Marshal.PtrToStructure(IntPtr.Add(ArrayPtr, PartitionSize * index), typeof(PartitionInformationEx)))
                            .ToArray());
                        return;
                    }
                    var ErrorCode = Marshal.GetHRForLastWin32Error();
                    if (ErrorCode == ERROR_INSUFFICIENT_BUFFER)
                    {
                        Size *= 2;
                        continue;
                    }
                    else
                    {
                        Marshal.ThrowExceptionForHR(ErrorCode);
                    }
                }
            }
            layout = default;
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_disk_get_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DriveLayoutInformationEx DiskGetDriveLayoutEx(this IoControl IoControl)
        {
            DiskGetDriveLayoutEx(IoControl, out var layout);
            return layout;
        }
        /// <summary>
        /// IOCTL_DISK_SET_DRIVE_LAYOUT_EX IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_set_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static void DiskSetDriveLayoutEx(this IoControl IoControl, ref DriveLayoutInformationEx layout)
        {
            var result = IoControl.DeviceIoControl(IOControlCode.DiskSetDriveLayoutEx, ref layout, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_DISK_GET_LENGTH_INFO IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_length_info )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="Length"></param>
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
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_drive_geometry_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="geometry"></param>
        public static void DiskGetDriveGeometryEx2(this IoControl IoControl, out DiskGeometryEx2 geometry)
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
        public static DiskGeometryEx2 DiskGetDriveGeometryEx2(this IoControl IoControl)
        {
            DiskGetDriveGeometryEx2(IoControl, out var geometry);
            return geometry;
        }
        /// <summary>
        /// IOCTL_DISK_CONTROLLER_NUMBER IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_controller_number )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="number"></param>
        [Obsolete()]
        public static void DiskControllerNumber(this IoControl IoControl, out DiskControllerNumber number)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskControllerNumber, out number, out _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_DISK_CONTROLLER_NUMBER IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_controller_number )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
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
        /// <summary>
        /// IOCTL_DISK_PERFORMANCE_OFF IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_performance_off )
        /// </summary>
        /// <param name="IoControl"></param>
        public static void DiskPerformanceOff(this IoControl IoControl)
        {
            var result = IoControl.DeviceIoControl(IOControlCode.DiskPerformanceOff, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
    }
}
