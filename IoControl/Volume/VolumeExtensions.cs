using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using IoControl;
using static IoControl.IoControl;

namespace IoControl.Volume
{
    public static class VolumeExtensions
    {
        private static bool VolumeIs(IoControl IoControl, IOControlCode IOControlCode, params uint[] NotErrorCodes)
        {
            var result = IoControl.DeviceIoControl(IOControlCode, out var _);
            if (!result)
            {
                var win32error = Marshal.GetHRForLastWin32Error();
                if (!NotErrorCodes.Any(ErrorCode => unchecked((uint)win32error) == ErrorCode))
                    Marshal.ThrowExceptionForHR(win32error);
            }
            return result;
        }
        /// <summary>
        /// IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddvol/ni-ntddvol-ioctl_volume_get_volume_disk_extents )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="extent"></param>
        public static void VolumeGetVolumeDiskExtents(this IoControl IoControl, out VolumeDiskExtent extent)
        {
            const int ERROR_INSUFFICIENT_BUFFER = 122;
            const int ERROR_MORE_DATA = 234;
            var Size = (uint)Marshal.SizeOf(typeof(VolumeDiskExtent));
            var OutPtr = Marshal.AllocCoTaskMem((int)Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(OutPtr)))
            {
                var get_volume_disk_result = IoControl.DeviceIoControlOutOnly(IOControlCode.VolumeGetVolumeDiskExtents, OutPtr, Size, out var ReturnBytes);
                var query_size_error = Marshal.GetHRForLastWin32Error();
                System.Diagnostics.Debug.WriteLine($"{nameof(get_volume_disk_result)}:{get_volume_disk_result}");
                System.Diagnostics.Debug.WriteLine($"{nameof(ReturnBytes)}:{ReturnBytes}");
                System.Diagnostics.Debug.WriteLine($"{nameof(query_size_error)}:0x{unchecked((uint)query_size_error):X8}");
                extent = (VolumeDiskExtent)Marshal.PtrToStructure(OutPtr, typeof(VolumeDiskExtent));
                System.Diagnostics.Debug.WriteLine($"{nameof(extent.NumberOfDiskExtents)}:{extent.NumberOfDiskExtents}");
                if (get_volume_disk_result && extent.NumberOfDiskExtents == 1)
                {
                    return;
                }
                if (query_size_error != ERROR_INSUFFICIENT_BUFFER && query_size_error != ERROR_MORE_DATA)
                {
                    Marshal.ThrowExceptionForHR(query_size_error);
                    return;
                }
            }
            Size = (uint)(Marshal.SizeOf(typeof(VolumeDiskExtent)) + Marshal.SizeOf(typeof(DiskExtent)) * (extent.NumberOfDiskExtents - 1));
            OutPtr = Marshal.AllocCoTaskMem((int)Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(OutPtr)))
            {
                var devide_ioc_result = IoControl.DeviceIoControlOutOnly(IOControlCode.VolumeGetVolumeDiskExtents, OutPtr, Size, out var ReturnBytes);
                if (!devide_ioc_result)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                extent = (VolumeDiskExtent)Marshal.PtrToStructure(OutPtr, typeof(VolumeDiskExtent));
                var ExtentsPtr = IntPtr.Add(OutPtr, Marshal.OffsetOf<VolumeDiskExtent>(nameof(VolumeDiskExtent.Extents)).ToInt32());
                var ExtentSize = Marshal.SizeOf(typeof(DiskExtent));
                extent.Extents = Enumerable
                        .Range(0, (int)extent.NumberOfDiskExtents)
                        .Select(index => (DiskExtent)Marshal.PtrToStructure(IntPtr.Add(ExtentsPtr, ExtentSize * index), typeof(DiskExtent)))
                        .ToArray();
                return;
            }
        }
        /// <summary>
        /// IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddvol/ni-ntddvol-ioctl_volume_get_volume_disk_extents )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static VolumeDiskExtent VolumeGetVolumeDiskExtents(this IoControl IoControl)
        {
            VolumeGetVolumeDiskExtents(IoControl, out var extent);
            return extent;
        }
        /// <summary>
        /// IOCTL_VOLUME_SUPPORTS_ONLINE_OFFLINE
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool VolumeSupportsOnlineOffline(this IoControl IoControl) => VolumeIs(IoControl, IOControlCode.VolumeSupportsOnlineOffline, 0x8007001F);
        /// <summary>
        /// IOCTL_VOLUME_ONLINE IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_volume_online )
        /// </summary>
        /// <param name="IoControl"></param>
        public static void VolumeOnline(this IoControl IoControl)
        {
            if (!IoControl.DeviceIoControl(IOControlCode.VolumeOnline, out var _))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_VOLUME_OFFLINE IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddvol/ni-ntddvol-ioctl_volume_offline )
        /// </summary>
        /// <param name="IoControl"></param>
        public static void VolumeOffline(this IoControl IoControl)
        {
            if (!IoControl.DeviceIoControl(IOControlCode.VolumeOffline, out var _))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_VOLUME_IS_OFFLINE IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool VolumeIsOffline(this IoControl IoControl) => VolumeIs(IoControl, IOControlCode.VolumeIsOffline, 0x8007001F);
        /// <summary>
        /// IOCTL_VOLUME_IS_IO_CAPABLE IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool VolumeIsIoCapale(this IoControl IoControl) => VolumeIs(IoControl, IOControlCode.VolumeIsIoCapable, 0x8007001F);
        /// <summary>
        /// IOCTL_VOLUME_QUERY_FAILOVER_SET IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="set"></param>
        public static void VolumeQueryFailoverSet(this IoControl IoControl, out VolumeFailoverSet set)
        {
            const int ERROR_INSUFFICIENT_BUFFER = 122;
            const int ERROR_MORE_DATA = 234;
            var Size = (uint)Marshal.SizeOf(typeof(VolumeFailoverSet));
            var OutPtr = Marshal.AllocCoTaskMem((int)Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(OutPtr)))
            {
                var get_volume_disk_result = IoControl.DeviceIoControlOutOnly(IOControlCode.VolumeGetVolumeDiskExtents, OutPtr, Size, out var ReturnBytes);
                var query_size_error = Marshal.GetHRForLastWin32Error();
                System.Diagnostics.Trace.WriteLine($"{nameof(get_volume_disk_result)}:{get_volume_disk_result}");
                System.Diagnostics.Trace.WriteLine($"{nameof(ReturnBytes)}:{ReturnBytes}");
                System.Diagnostics.Trace.WriteLine($"{nameof(query_size_error)}:0x{unchecked((uint)query_size_error):X8}");
                set = (VolumeFailoverSet)Marshal.PtrToStructure(OutPtr, typeof(VolumeFailoverSet));
                System.Diagnostics.Trace.WriteLine($"{nameof(set.NumberOfDisks)}:{set.NumberOfDisks}");
                if (get_volume_disk_result && set.NumberOfDisks == 0)
                {
                    set.DiskNumbers = new uint[0];
                    return;
                }
                if (get_volume_disk_result && set.NumberOfDisks == 1)
                {
                    return;
                }
                if (query_size_error != ERROR_INSUFFICIENT_BUFFER && query_size_error != ERROR_MORE_DATA)
                {
                    Marshal.ThrowExceptionForHR(query_size_error);
                    return;
                }
            }
            Size = (uint)(Marshal.SizeOf(typeof(VolumeFailoverSet)) + sizeof(uint) * (set.NumberOfDisks - 1));
            OutPtr = Marshal.AllocCoTaskMem((int)Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(OutPtr)))
            {
                var devide_ioc_result = IoControl.DeviceIoControlOutOnly(IOControlCode.VolumeGetVolumeDiskExtents, OutPtr, Size, out var ReturnBytes);
                if (!devide_ioc_result)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                set = (VolumeFailoverSet)Marshal.PtrToStructure(OutPtr, typeof(VolumeFailoverSet));
                var ExtentsPtr = IntPtr.Add(OutPtr, Marshal.OffsetOf<VolumeFailoverSet>(nameof(VolumeFailoverSet.DiskNumbers)).ToInt32());
                var ExtentSize = Marshal.SizeOf(typeof(DiskExtent));
                set.DiskNumbers = Enumerable
                        .Range(0, (int)set.NumberOfDisks)
                        .Select(index => (uint)Marshal.PtrToStructure(IntPtr.Add(ExtentsPtr, ExtentSize * index), typeof(uint)))
                        .ToArray();
                return;
            }
        }
        /// <summary>
        /// IOCTL_VOLUME_QUERY_FAILOVER_SET IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static VolumeFailoverSet VolumeQueryFailoverSet(this IoControl IoControl)
        {
            VolumeQueryFailoverSet(IoControl, out var set);
            return set;
        }
        /// <summary>
        /// IOCTL_VOLUME_QUERY_VOLUME_NUMBER IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="number"></param>
        public static void VolumeQueryVolumeNumber(this IoControl IoControl, out VolumeNumber number)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.VolumeQueryVolumeNumber, out number, out var ReturnBytes);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_VOLUME_QUERY_VOLUME_NUMBER IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static VolumeNumber VolumeQueryVolumeNumber(this IoControl IoControl)
        {
            VolumeQueryVolumeNumber(IoControl, out var number);
            return number;
        }
        /// <summary>
        /// IOCTL_VOLUME_LOGICAL_TO_PHYSICAL IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="logial"></param>
        /// <param name="physical"></param>
        public static void VolumeLogicalToPhysical(this IoControl IoControl, in VolumeLogicalOffset logial, out VolumePhysicalOffsets physical)
        {
            const int ERROR_INSUFFICIENT_BUFFER = 122;
            const int ERROR_MORE_DATA = 234;
            var InSize = (uint)Marshal.SizeOf(typeof(VolumeLogicalOffset));
            var InPtr = Marshal.AllocCoTaskMem((int)InSize);
            var OutSize = (uint)Marshal.SizeOf(typeof(VolumePhysicalOffsets));
            var OutPtr = Marshal.AllocCoTaskMem((int)OutSize);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(InPtr)))
            {
                Marshal.StructureToPtr(logial, InPtr, false);
                using (Disposable.Create(() => Marshal.FreeCoTaskMem(OutPtr)))
                {
                    var result = IoControl.DeviceIoControl(IOControlCode.VolumeLogicalToPhysical, InPtr, InSize, OutPtr, OutSize, out var ReturnBytes);
                    var query_size_error = Marshal.GetHRForLastWin32Error();
                    System.Diagnostics.Trace.WriteLine($"{nameof(result)}:{result}");
                    System.Diagnostics.Trace.WriteLine($"{nameof(ReturnBytes)}:{ReturnBytes}");
                    System.Diagnostics.Trace.WriteLine($"{nameof(query_size_error)}:0x{unchecked((uint)query_size_error):X8}");
                    physical = (VolumePhysicalOffsets)Marshal.PtrToStructure(OutPtr, typeof(VolumePhysicalOffsets));
                    System.Diagnostics.Trace.WriteLine($"{nameof(physical.NumberOfPhysicalOffsets)}:{physical.NumberOfPhysicalOffsets}");
                    if (result && physical.NumberOfPhysicalOffsets == 0)
                    {
                        physical.PhysicalOffset = new VolumePhysicalOffset[0];
                        return;
                    }
                    if (result && physical.NumberOfPhysicalOffsets == 1)
                    {
                        return;
                    }
                    if (query_size_error != ERROR_INSUFFICIENT_BUFFER && query_size_error != ERROR_MORE_DATA)
                    {
                        Marshal.ThrowExceptionForHR(query_size_error);
                        return;
                    }
                }
                OutSize = (uint)(Marshal.SizeOf(typeof(VolumePhysicalOffsets)) + Marshal.SizeOf(typeof(VolumePhysicalOffset)) * (physical.NumberOfPhysicalOffsets - 1));
                OutPtr = Marshal.AllocCoTaskMem((int)OutSize);
                using (Disposable.Create(() => Marshal.FreeCoTaskMem(OutPtr)))
                {
                    var devide_ioc_result = IoControl.DeviceIoControl(IOControlCode.VolumeLogicalToPhysical, InPtr, InSize, OutPtr, OutSize, out var ReturnBytes);
                    if (!devide_ioc_result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    physical = (VolumePhysicalOffsets)Marshal.PtrToStructure(OutPtr, typeof(VolumePhysicalOffsets));
                    var ExtentsPtr = IntPtr.Add(OutPtr, Marshal.OffsetOf<VolumeFailoverSet>(nameof(VolumeFailoverSet.DiskNumbers)).ToInt32());
                    var ExtentSize = Marshal.SizeOf(typeof(VolumePhysicalOffset));
                    physical.PhysicalOffset = Enumerable
                            .Range(0, (int)physical.NumberOfPhysicalOffsets)
                            .Select(index => (VolumePhysicalOffset)Marshal.PtrToStructure(IntPtr.Add(ExtentsPtr, ExtentSize * index), typeof(VolumePhysicalOffset)))
                            .ToArray();
                    return;
                }
            }
        }
        /// <summary>
        /// IOCTL_VOLUME_LOGICAL_TO_PHYSICAL IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="logical"></param>
        /// <returns></returns>
        public static VolumePhysicalOffsets VolumeLogicalToPhysical(this IoControl IoControl, in VolumeLogicalOffset logical)
        {
            VolumeLogicalToPhysical(IoControl, in logical, out var physical);
            return physical;
        }
        /// <summary>
        /// IOCTL_VOLUME_PHYSICAL_TO_LOGICAL IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="physical"></param>
        /// <param name="logical"></param>
        public static void VolumePhysicalToLogical(this IoControl IoControl, in VolumePhysicalOffset physical, out VolumeLogicalOffset logical)
        {
            var result = IoControl.DeviceIoControl(IOControlCode.VolumePhysicalToLogical, in physical, out logical, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_VOLUME_PHYSICAL_TO_LOGICAL IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="physical"></param>
        /// <returns></returns>
        public static VolumeLogicalOffset VolumePhysicalToLogical(this IoControl IoControl, in VolumePhysicalOffset physical)
        {
            VolumePhysicalToLogical(IoControl, in physical, out var logical);
            return logical;
        }
        /// <summary>
        /// IOCTL_VOLUME_IS_PARTITION IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool VolumeIsPartition(this IoControl IoControl) => VolumeIs(IoControl, IOControlCode.VolumeIsPartition, 0x8007001F);
        /// <summary>
        /// IOCTL_VOLUME_READ_PLEX IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddvol/ni-ntddvol-ioctl_volume_read_plex )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="input"></param>
        public static void VolumeReadPlex(this IoControl IoControl, in VolumeReadPlexInput input) => throw new NotImplementedException();
        /// <summary>
        /// IOCTL_VOLUME_IS_CLUSTERED IOCTL ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddvol/ni-ntddvol-ioctl_volume_is_clustered )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool VolumeIsClustered(this IoControl IoControl) => VolumeIs(IoControl, IOControlCode.VolumeIsClustered, 0x8007001F);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="GptAttributes"></param>
        public static void VolumeSetGptAttribute(this IoControl IoControl, in VolumeSetGptAttributesInformation information)
        {
            var result = IoControl.DeviceIoControlInOnly(IOControlCode.VolumeSetGptAttribute, in information, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static void VolumeSetGptAttribute(this IoControl IoControl, Disk.EFIPartitionAttributes GptAttributes, bool RevertOnClose = default, bool ApplyToAllConnectedVolumes = default)
        {
            VolumeSetGptAttribute(IoControl, new VolumeSetGptAttributesInformation 
            {
                GptAttributes = GptAttributes,
                RevertOnClose = RevertOnClose,
                ApplyToAllConnectedVolumes = ApplyToAllConnectedVolumes,
            });
        }
        /// <summary>
        /// IOCTL_VOLUME_GET_GPT_ATTRIBUTES IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_volume_get_gpt_attributes )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="attribute"></param>
        public static void VolumeGetGptAttribute(this IoControl IoControl, out Disk.EFIPartitionAttributes attribute)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.VolumeGetGptAttribute, out VolumeGetGptAttributesInformation _attribute, out var _);
            attribute = _attribute.GptAttributes;
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        /// <summary>
        /// IOCTL_VOLUME_GET_GPT_ATTRIBUTES IOCTL ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_volume_get_gpt_attributes )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static Disk.EFIPartitionAttributes VolumeGetGptAttribute(this IoControl IoControl)
        {
            VolumeGetGptAttribute(IoControl, out var attribute);
            return attribute;
        }
    }
}
