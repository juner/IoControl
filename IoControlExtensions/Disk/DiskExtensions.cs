using System;
using System.Collections.Generic;
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
        internal const int READ_ATTRIBUTE_BUFFER_SIZE = 512;
        internal const int IDENTIFY_BUFFER_SIZE = 512;
        internal const int READ_THRESHOLD_BUFFER_SIZE = 512;
        /// <summary>
        /// IOCTL_DISK_ARE_VOLUMES_READY IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/fileio/ioctl-disk-are-volumes-ready )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static Task<bool> DiskAreVolumesReadyAsync(this IoControl IoControl, CancellationToken Token = default) => IoControl.DeviceIoControlAsync(IOControlCode.DiskAreVolumesReady, Token: Token);
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
            if (!DiskGetCacheInformation(IoControl, out var information))
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
            if (!DiskGetPartitionInfo(IoControl, out var partition, out var ReturnBytes) && ReturnBytes > 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return partition;
        }
        public static bool DiskSetPartitionInfo(this IoControl IoControl, in PartitionInformation partition, out uint ReturnBytes) => IoControl.DeviceIoControlInOnly(IOControlCode.DiskSetPartitionInfo, partition, out ReturnBytes);
        public static bool DiskGetPartitionInfoEx(this IoControl IoControl, out PartitionInformationEx partition, out uint ReturnBytes) => IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetPartitionInfoEx, out partition, out ReturnBytes);
        public static PartitionInformationEx DiskGetPartitionInfoEx(this IoControl IoControl)
        {
            if (!DiskGetPartitionInfoEx(IoControl, out var partition, out var ReturnBytes) && ReturnBytes > 0)
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
            var data = new DataUtils.AnySizeStruct<DriveLayoutInformation>();
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveLayout, data, out ReturnBytes);
            layout = data.Get();
            return result;
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_get_drive_layout )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static DriveLayoutInformation? DiskGetDriveLayout(this IoControl IoControl) => !DiskGetDriveLayout(IoControl, out var layout, out var ReturnBytes) && ReturnBytes == 0 ? (DriveLayoutInformation?)null : layout;
        /// <summary>
        /// IOCTL_DISK_SET_DRIVE_LAYOUT IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_set_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static bool DiskSetDriveLayout(this IoControl IoControl, in DriveLayoutInformation layout)
        {
            using (layout.CreatePtr(out var Ptr, out var Size))
                return IoControl.DeviceIoControlInOnly(IOControlCode.DiskSetDriveLayout, Ptr, Size, out var _);
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_disk_get_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static bool DiskGetDriveLayoutEx(this IoControl IoControl, out DriveLayoutInformationEx layout, out uint ReturnBytes)
        {
            var data = new DataUtils.AnySizeStruct<DriveLayoutInformationEx>();
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveLayoutEx, data, out ReturnBytes);
            layout = data.Get();
            return result;
        }
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_LAYOUT_EX IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_disk_get_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DriveLayoutInformationEx? DiskGetDriveLayoutEx(this IoControl IoControl) => !DiskGetDriveLayoutEx(IoControl, out var layout, out var ReturnBytes) && ReturnBytes == 0 ? (DriveLayoutInformationEx?)null : layout;
        /// <summary>
        /// IOCTL_DISK_SET_DRIVE_LAYOUT_EX IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ni-ntdddisk-ioctl_disk_set_drive_layout_ex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="layout"></param>
        public static bool DiskSetDriveLayoutEx(this IoControl IoControl, in DriveLayoutInformationEx layout)
        {
            using (layout.CreatePtr(out var Ptr, out var Size))
                return IoControl.DeviceIoControlInOnly(IOControlCode.DiskSetDriveLayoutEx, Ptr, Size, out var _);

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
        public static bool DiskGetDriveGeometry(this IoControl IoControl, out DiskGeometry geometry, out uint ReturnBytes) => IoControl.DeviceIoControlOutOnly(IOControlCode.DiskGetDriveGeometry, out geometry, out ReturnBytes);
        /// <summary>
        /// IOCTL_DISK_GET_DRIVE_GEOMETRY IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static DiskGeometry? DiskGetDriveGeometry(this IoControl IoControl) => !DiskGetDriveGeometry(IoControl, out var geometry, out var ReturnBytes) && ReturnBytes == 0 ? (DiskGeometry?)null : geometry;
        public static async Task<DiskGeometry?> DiskGetDriveGeometryAsync(this IoControl IoControl, CancellationToken Token = default)
        {
            var (Result, DiskGeometry, ReturnBytes) = await IoControl.DeviceIoControlIOutOnlyAsync<DiskGeometry>(IOControlCode.DiskGetDriveGeometry, Token);
            return !Result && ReturnBytes == 0 ? (DiskGeometry?)null : DiskGeometry;
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
        public static (DiskGeometry Geometry, long DiskSize) DiskGetDriveGeometryEx(this IoControl IoControl)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="Versioninparams"></param>
        /// <returns></returns>
        public static bool SmartGetVersion(this IoControl IoControl, out Getversioninparams Versioninparams)
            => IoControl.DeviceIoControlOutOnly(IOControlCode.SmartGetVersion, out Versioninparams, out var ReturnBytes) && ReturnBytes > 0;
        public static IdentifyDeviceOutData SmartRcvDriveDataIdentifyDevice(this IoControl IoControl, byte Target)
        {
            if (!IoControl.SmartRcvDriveDataIdentifyDevice(Target, out var IdentifyDevice))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return IdentifyDevice;
        }
        public static bool SmartRcvDriveDataIdentifyDevice(this IoControl IoControl, byte Target, out IdentifyDeviceOutData IdentifyDeviceOutData)
        {
            const byte ID_CMD = 0xEC;
            var indata = new Sendcmdinparams(
                BufferSize: IDENTIFY_BUFFER_SIZE,
                DriveRegs: new Ideregs(
                    CommandReg: ID_CMD,
                    SectorCountReg: 1,
                    SectorNumberReg: 1,
                    DriveHeadReg: Target
                ));
            var inData = new DataUtils.StructPtr<Sendcmdinparams>(indata);
            var outData = new DataUtils.StructPtr<IdentifyDeviceOutData>();
            var result = IoControl.SmartRcvDriveData(inData, outData);
            IdentifyDeviceOutData = outData.Get();
            return result;
        }
        public static SmartReadDataOutData SmartRcvDriveDataSmartReadData(this IoControl IoControl, byte Target)
        {
            if (!IoControl.SmartRcvDriveDataSmartReadData(Target, out var OutData))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return OutData;
        }
        public static bool SmartRcvDriveDataSmartReadData(this IoControl IoControl, byte Target, out SmartReadDataOutData Attributes)
        {
            const uint READ_THRESHOLD_BUFFER_SIZE = 512;
            const byte SMART_CMD = 0xB0;
            const byte SMART_CYL_LOW = 0x4F;
            const byte SMART_CYL_HI = 0xC2;
            var indata = new Sendcmdinparams(
                BufferSize: READ_THRESHOLD_BUFFER_SIZE,
                DriveRegs: new Ideregs(
                    CommandReg: SMART_CMD,
                    SectorCountReg: 1,
                    SectorNumberReg: 1,
                    CylLowReg: SMART_CYL_LOW,
                    CylHighReg: SMART_CYL_HI,
                    DriveHeadReg: Target
                ));
            var inData = new DataUtils.StructPtr<Sendcmdinparams>(indata);
            var outData = new DataUtils.StructPtr<SmartReadDataOutData>();
            var result = IoControl.SmartRcvDriveData(inData, outData);
            Attributes = outData.Get();
            return result;
        }
        /// <summary>
        /// SMART_RCV_DRIVE_DATA control code
        /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff566204.aspx
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="inparams"></param>
        /// <param name="outparams"></param>
        /// <returns></returns>
        public static bool SmartRcvDriveData<IN,OUT>(this IoControl IoControl, DataUtils.StructPtr<IN> inparams, DataUtils.StructPtr<OUT> outparams)
            where IN : struct, ISendcmdinparams
            where OUT : struct, ISendcmdoutparams
            => IoControl.DeviceIoControl(IOControlCode.SmartRcvDriveData, inparams, outparams, out var ReturnBytes) && ReturnBytes > 0;
    }
}