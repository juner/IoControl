using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        public static Task<bool> DiskAreVolumesReadyAsync(this IoControl IoControl, CancellationToken Token = default) => IoControl.DeviceIoControlAsync(IOControlCode.DiskAreVolumesReady,Token:Token);
        /// <summary>
        /// IOCTL_DISK_ARE_VOLUMES_READY IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/fileio/ioctl-disk-are-volumes-ready )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool DiskAreVolumeReady(this IoControl IoControl) => IoControl.DeviceIoControl(IOControlCode.DiskAreVolumesReady, out _);
        /// <summary>
        /// IOCTL_DISK_VOLUMES_ARE_READY IOCTL 
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="argment"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static Task<bool> DiskVolumesAreReadyAsync(this IoControl IoControl, uint argment, CancellationToken Token = default) => throw new NotImplementedException();
        public static void CreateDisk(this IoControl IoControl, in CreateDisk CreateDisk) => throw new NotImplementedException();
        /// <summary>
        /// IOCTL_DISK_GET_CACHE_INFORMATION IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_cache_information )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="information"></param>
        public static bool DiskGetCacheInformation(this IoControl IoControl, out DiskCacheInformation information) => IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetCacheInformation, out information, out var _);
        /// <summary>
        /// IOCTL_DISK_GET_CACHE_INFORMATION IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_cache_information )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DiskCacheInformation DiskGetCacheInformation(this IoControl IoControl)
        {
            if(!DiskGetCacheInformation(IoControl, out var information))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return information;
        }
        /// <summary>
        /// IOCTL_DISK_GET_CACHE_INFORMATION IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_cache_information )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool DiskSetCacheInformation(this IoControl IoControl, ref DiskCacheInformation information, out uint ReturnBytes) => IoControl.DeviceIoControl(IOControlCode.DiskSetCacheInformation, ref information, out ReturnBytes);
        public static bool DiskGetPartitionInfo(this IoControl IoControl, out PartitionInformation partition, out uint ReturnBytes) => IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetPartitionInfo, out partition, out ReturnBytes);
        public static PartitionInformation DiskGetPartitionInfo(this IoControl IoControl)
        {
            if(!DiskGetPartitionInfo(IoControl, out var partition, out var ReturnBytes) && ReturnBytes > 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return partition;
        }
        public static bool DiskSetPartitionInfo(this IoControl IoControl, in PartitionInformation partition, out uint ReturnBytes) => IoControl.DeviceIoControlInOnly(IOControlCode.DiskSetPartitionInfo, partition, out ReturnBytes);
        public static bool DiskGetPartitionInfoEx(this IoControl IoControl, out PartitionInformationEx partition, out uint ReturnBytes) => IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetPartitionInfoEx, out partition, out ReturnBytes);
        public static PartitionInformationEx DiskGetPartitionInfoEx(this IoControl IoControl)
        {
            if(!DiskGetPartitionInfoEx(IoControl, out var partition, out var ReturnBytes) && ReturnBytes > 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return partition;
        }
        public static bool DiskSetPartitionEx(this IoControl IoControl, in PartitionInformationEx partition) => IoControl.DeviceIoControlInOnly(IOControlCode.DiskSetPartitionInfoEx, partition, out var _);
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_get_drive_layout )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static bool DiskGetDriveLayout(this IoControl IoControl, out DriveLayoutInformation layout, out uint ReturnBytes)
        {
            DriveLayoutInformation AddArray(IntPtr Ptr, DriveLayoutInformation _layout)
            {
                var ArrayPtr = IntPtr.Add(Ptr, Marshal.OffsetOf<DriveLayoutInformation>(nameof(_layout._PartitionEntry)).ToInt32());
                var PartitionSize = Marshal.SizeOf<PartitionInformation>();
                return _layout.Set(
                        PartitionEntry: Enumerable
                        .Range(0, (int)_layout.PartitionCount)
                        .Select(index => (PartitionInformation)Marshal.PtrToStructure(IntPtr.Add(ArrayPtr, PartitionSize * index), typeof(PartitionInformation)))
                        .ToArray()
                    );
            }
            return IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveLayout, out layout, AddArray, out ReturnBytes);
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_get_drive_layout )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static DriveLayoutInformation DiskGetDriveLayout(this IoControl IoControl)
        {
            if (!DiskGetDriveLayout(IoControl, out var layout, out var ReturnBytes) && ReturnBytes == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return layout;
        }
        /// <summary>
        /// IOCTL_DISK_SET_DRIVE_LAYOUT IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_set_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static bool DiskSetDriveLayout(this IoControl IoControl, in DriveLayoutInformation layout)
        {
            var PartitionSize = Marshal.SizeOf<PartitionInformation>();
            var Size = (uint)(Marshal.SizeOf<DriveLayoutInformation>()
                + PartitionSize * Math.Max(((int)layout.PartitionCount - 1), 0));
            var Ptr = Marshal.AllocCoTaskMem((int)Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
            {
                Marshal.StructureToPtr(layout, Ptr, false);
                var _Ptr = IntPtr.Add(Ptr, (int)Marshal.OffsetOf<DriveLayoutInformation>(nameof(layout._PartitionEntry)));
                foreach (var (partition, index) in layout.PartitionEntry.Select((p, i) => (p, i)).Skip(1))
                    Marshal.StructureToPtr(partition, IntPtr.Add(_Ptr, PartitionSize * index), false);
                return IoControl.DeviceIoControlInOnly(IOControlCode.DiskSetDriveLayout, Ptr, Size, out var _);
            }
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_disk_get_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static bool DiskGetDriveLayoutEx(this IoControl IoControl, out DriveLayoutInformationEx layout, out uint ReturnBytes)
        {
            DriveLayoutInformationEx AddArray(IntPtr Ptr, DriveLayoutInformationEx _layout)
            {
                var offset = (int)Marshal.OffsetOf<DriveLayoutInformationEx>(nameof(layout._PartitionEntry));
                var ArrayPtr = IntPtr.Add(Ptr, offset);
                var PartitionSize = Marshal.SizeOf<PartitionInformationEx>();
                return _layout.Set(PartitionEntry: Enumerable
                    .Range(0, (int)_layout.PartitionCount)
                    .Select(index => (PartitionInformationEx)Marshal.PtrToStructure(IntPtr.Add(ArrayPtr, PartitionSize * index), typeof(PartitionInformationEx)))
                    .ToArray());
            }
            return IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveLayoutEx, out layout, AddArray, out ReturnBytes);
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_disk_get_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DriveLayoutInformationEx DiskGetDriveLayoutEx(this IoControl IoControl)
        {
            if (!DiskGetDriveLayoutEx(IoControl, out var layout, out var ReturnBytes) && ReturnBytes == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return layout;
        }
        /// <summary>
        /// IOCTL_DISK_SET_DRIVE_LAYOUT_EX IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_set_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static bool DiskSetDriveLayoutEx(this IoControl IoControl, in DriveLayoutInformationEx layout)
        {
            var PartitionSize = Marshal.SizeOf<PartitionInformationEx>();
            var Size = (uint)(Marshal.SizeOf<DriveLayoutInformationEx>()
                + PartitionSize * Math.Max(((int)layout.PartitionCount - 1), 0));
            var Ptr = Marshal.AllocCoTaskMem((int)Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
            {
                Marshal.StructureToPtr(layout, Ptr, false);
                var _Ptr = IntPtr.Add(Ptr, (int)Marshal.OffsetOf<DriveLayoutInformationEx>(nameof(layout._PartitionEntry)));
                foreach(var (partition, index) in layout.PartitionEntry.Select((p,i)=> (p,i)).Skip(1))
                    Marshal.StructureToPtr(partition, IntPtr.Add(_Ptr, PartitionSize * index), false);
                return IoControl.DeviceIoControlInOnly(IOControlCode.DiskSetDriveLayoutEx, Ptr, Size, out var _);
            }
        }
        /// <summary>
        /// IOCTL_DISK_GET_LENGTH_INFO IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_length_info )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="Length"></param>
        public static bool DiskGetLengthInfo(this IoControl IoControl, out long Length) => IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetLengthInfo, out Length, out _);
        /// <summary>
        /// IOCTL_DISK_GET_LENGTH_INFO IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_length_info )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static long DiskGetLengthInfo(this IoControl IoControl)
        {
            if (!DiskGetLengthInfo(IoControl, out var Length))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return Length;
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="geometry"></param>
        public static bool DiskGetDriveGeometry(this IoControl IoControl, out DiskGeometry geometry) => IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveGeometry, out geometry, out _);
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DiskGeometry DiskGetDriveGeometry(this IoControl IoControl)
        {
            if (!DiskGetDriveGeometry(IoControl, out var geometry))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return geometry;
        }
        /// <summary>
        /// 
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_drive_geometry_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="geometry"></param>
        public static bool DiskGetDriveGeometryEx(this IoControl IoControl, out DiskGeometry Geometry, out long DiskSize, out uint ByteSize)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveGeometryEx, out DiskGeometryEx geometryEx, out ByteSize);
            if (ByteSize > 0)
                (Geometry, DiskSize) = geometryEx;
            else
                (Geometry, DiskSize) = (default, default);
            return result;
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_drive_geometry_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static (DiskGeometry Geometry, long DiskSize)  DiskGetDriveGeometryEx(this IoControl IoControl)
        {
            if (!DiskGetDriveGeometryEx(IoControl, out var geometry, out var DiskSize, out var ByteSize) && ByteSize == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return (geometry, DiskSize);
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_drive_geometry_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="Geometry"></param>
        public static bool DiskGetDriveGeometryEx(this IoControl IoControl, out DiskGeometry Geometry, out long Size, out DiskPartitionInfo PartitionInfo, out DiskDetectionInfo DetectionInfo, out uint ByteSize)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveGeometryEx, out DiskGeometryEx2 geometryEx, out ByteSize);
            if (ByteSize > 0)
                (Geometry, Size, PartitionInfo, DetectionInfo) = geometryEx;
            else
                (Geometry, Size, PartitionInfo, DetectionInfo) = (default, default, default, default);
            return result;
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_get_drive_geometry_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static (DiskGeometry Geometry, long Size, DiskPartitionInfo PartitionInfo, DiskDetectionInfo DetectionInfo) DiskGetDriveGeometryEx2(this IoControl IoControl)
        {
            if (!DiskGetDriveGeometryEx(IoControl, out var Geometry, out var Size, out var PartitionInfo, out var DetectionInfo, out var ByteSize) && ByteSize == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return (Geometry, Size, PartitionInfo, DetectionInfo);
        }
        /// <summary>
        /// IOCTL_DISK_CONTROLLER_NUMBER IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_controller_number )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="number"></param>
        [Obsolete()]
        public static bool DiskControllerNumber(this IoControl IoControl, out DiskControllerNumber number) => IoControl.DeviceIoControlOutOnly(IOControlCode.DiskControllerNumber, out number, out _);
        /// <summary>
        /// IOCTL_DISK_CONTROLLER_NUMBER IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_controller_number )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        [Obsolete()]
        public static DiskControllerNumber DiskControllerNumber(this IoControl IoControl)
        {
            if (!DiskControllerNumber(IoControl, out var number))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return number;
        }
        /// <summary>
        /// IOCTL_DISK_PERFORMANCE IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_performance )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="performance"></param>
        public static bool DiskPerformance(this IoControl IoControl, out DiskPerformance performance) => IoControl.DeviceIoControlOutOnly(IOControlCode.DiskPerformance, out performance, out _);
        /// <summary>
        /// IOCTL_DISK_PERFORMANCE IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/WinIoCtl/ni-winioctl-ioctl_disk_performance )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DiskPerformance DiskPerformance(this IoControl IoControl)
        {
            if (!DiskPerformance(IoControl, out var performance))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return performance;
        }
        /// <summary>
        /// IOCTL_DISK_PERFORMANCE_OFF IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_performance_off )
        /// </summary>
        /// <param name="IoControl"></param>
        public static bool DiskPerformanceOff(this IoControl IoControl) => IoControl.DeviceIoControl(IOControlCode.DiskPerformanceOff, out var _);
    }
}
