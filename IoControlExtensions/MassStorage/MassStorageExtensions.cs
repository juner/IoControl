using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static IoControl.IoControl;

namespace IoControl.MassStorage
{
    public static class MassStorageExtensions
    {
        /// <summary>
        /// IOCTL_STORAGE_GET_HOTPLUG_INFO IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="info"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public static bool StorageGetHotplugInfo(this IoControl IoControl, out StorageHotplugInfo info, out uint ReturnBytes) => IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetHotplugInfo, out info, out ReturnBytes);
        /// <summary>
        /// IOCTL_STORAGE_GET_HOTPLUG_INFO IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static StorageHotplugInfo StorageGetHotplugInfo(this IoControl IoControl)
        {
            if (!StorageGetHotplugInfo(IoControl, out var info, out var ReturnBytes) && ReturnBytes == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return info;
        }
        /// <summary>
        /// IOCTL_STORAGE_GET_DEVICE_NUMBER IOCTL
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_storage_get_device_number
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool StorageGetDeviceNumber(this IoControl IoControl, out StorageDeviceNumber number, out uint ReturnBytes) => IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetDeviceNumber, out number, out ReturnBytes);
        /// <summary>
        /// IOCTL_STORAGE_GET_DEVICE_NUMBER IOCTL
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_storage_get_device_number
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static StorageDeviceNumber StorageGetDeviceNumber(this IoControl IoControl)
        {
            if(!StorageGetDeviceNumber(IoControl, out var number, out var ReturnBytes) && ReturnBytes == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return number;
        }
        public static void StorageQueryProperty(this IoControl IoControl, StoragePropertyId PropertyId, StorageQueryType QueryType, byte[] AdditionalParameters, out StorageDeviceDescriptor descriptor)
        {
            var query = new StoragePropertyQuery (
                PropertyId: PropertyId,
                QueryType: QueryType,
                AdditionalParameters: AdditionalParameters ?? new byte[1]
            );
            StorageQueryProperty(IoControl, ref query, out descriptor);
        }
        public static StorageDeviceDescriptor StorageQueryProperty(this IoControl IoControl, StoragePropertyId PropertyId, StorageQueryType QueryType = default, params byte[] AdditionalParameters)
        {
            StorageQueryProperty(IoControl, PropertyId, QueryType, AdditionalParameters, out var descriptor);
            return descriptor;
        }
        public static void StorageQueryProperty(this IoControl IoControl, ref StoragePropertyQuery query, out StorageDeviceDescriptor descriptor)
        {
            var Pack = typeof(StoragePropertyQuery).StructLayoutAttribute.Pack;
            const int propertyIdSize = sizeof(StoragePropertyId);
            const int queryTypeSize = sizeof(StorageQueryType);
            int additionalSize = sizeof(byte) * (query.AdditionalParameters?.Length + 1) ?? 1;
            uint inSize = (uint)(propertyIdSize + queryTypeSize + additionalSize);
            if (inSize % Pack > 0) inSize = (uint)(int)(Math.Ceiling(inSize / (double)Pack) * Pack);

            var inPtr = Marshal.AllocCoTaskMem((int)inSize);
            uint outSize;
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(inPtr)))
            {
                var _inPtr = inPtr;
                Marshal.WriteInt32(_inPtr, unchecked((int)query.PropertyId));
                _inPtr += propertyIdSize;
                Marshal.WriteInt32(_inPtr, unchecked((int)query.QueryType));
                _inPtr += queryTypeSize;
                foreach (var elm in query.AdditionalParameters)
                {
                    Marshal.WriteByte(_inPtr, elm);
                    _inPtr += sizeof(byte);
                }
                Marshal.WriteByte(_inPtr, unchecked((byte)-1));
                var headerSize = (uint)Marshal.SizeOf(typeof(StorageDescriptorHeader));
                var headerPtr = Marshal.AllocCoTaskMem((int)headerSize);
                using (Disposable.Create(() => Marshal.FreeCoTaskMem(headerPtr)))
                {
                    
                    var result = IoControl.DeviceIoControl(IOControlCode.StorageQueryProperty, inPtr, inSize, headerPtr, headerSize, out var _);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    var header = (StorageDescriptorHeader)Marshal.PtrToStructure(headerPtr, typeof(StorageDescriptorHeader));
                    System.Diagnostics.Debug.WriteLine(header);
                    outSize = header.Size;
                }
                var outPtr = Marshal.AllocCoTaskMem((int)outSize);
                using (Disposable.Create(() => Marshal.FreeCoTaskMem(outPtr)))
                {
                    var result = IoControl.DeviceIoControl(IOControlCode.StorageQueryProperty, inPtr, inSize, outPtr, outSize, out var _);
                    if (!result)
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    var d = (StorageDeviceDescriptor)Marshal.PtrToStructure(outPtr, typeof(StorageDeviceDescriptor));
                    var raw = new byte[outSize - Marshal.SizeOf<StorageDeviceDescriptor>()];
                    var rawPtr = IntPtr.Add(outPtr, Marshal.OffsetOf<StorageDeviceDescriptor>(nameof(StorageDeviceDescriptor.RawDeviceProperties)).ToInt32());
                    Marshal.Copy(rawPtr, raw, 0, raw.Length);
                    descriptor = new StorageDeviceDescriptor(
                            d.Version,
                            d.Size,
                            d.DeviceType,
                            d.DeviceTypeModifier,
                            d.RemovableMedia,
                            d.CommandQueueing,
                            d.VendorIdOffset,
                            d.ProductIdOffset,
                            d.ProductRevisionOffset,
                            d.SerialNumberOffset,
                            d.BusType,
                            d.RawPropertiesLength,
                            raw
                        );
                    return;
                    
                }
            }
        }
        public static StorageDeviceDescriptor StorageQueryProperty(this IoControl IoControl, ref StoragePropertyQuery query)
        {
            StorageQueryProperty(IoControl, ref query, out var descriptor);
            return descriptor;
        }
        const int ERROR_INSUFFICIENT_BUFFER = unchecked((int)0x8007007A);

        /// <summary>
        /// IOCTL_STORAGE_GET_MEDIA_SERIAL_NUMBER IOCTL
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_storage_get_media_serial_number
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="MediaSerialNumberData"></param>
        /// <returns></returns>
        public static bool StorageGetMediaSerialNumber(this IoControl IoControl, out MediaSerialNumberData MediaSerialNumberData)
        {
            var Size = (uint)Marshal.SizeOf<MediaSerialNumberData>();
            var ReturnSize = 0u;
            while (ReturnSize == 0u)
            {
                var Ptr = Marshal.AllocCoTaskMem((int)Size);
                using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
                {
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetMediaSerialNumber, Ptr, Size, out ReturnSize);
                    if (result)
                    {
                        var offset = (int)Marshal.OffsetOf<MediaSerialNumberData>(nameof(MassStorage.MediaSerialNumberData.SerialNumberData));
                        MediaSerialNumberData = (MediaSerialNumberData)Marshal.PtrToStructure(Ptr, typeof(MediaSerialNumberData));
                        var SerialNumberData = new byte[MediaSerialNumberData.SerialNumberLength];
                        Marshal.Copy(IntPtr.Add(Ptr, offset), SerialNumberData, 0, SerialNumberData.Length);
                        MediaSerialNumberData = MediaSerialNumberData.Set(SerialNumberData: SerialNumberData);
                        return result;
                    }
                    var ErrorCode = Marshal.GetHRForLastWin32Error();
                    if (ErrorCode == ERROR_INSUFFICIENT_BUFFER)
                    {
                        Size *= 2;
                        continue;
                    }
                    else break;
                }
            }
            MediaSerialNumberData = default;
            return false;
        }
        /// <summary>
        /// IOCTL_STORAGE_GET_MEDIA_SERIAL_NUMBER IOCTL
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ni-winioctl-ioctl_storage_get_media_serial_number
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static MediaSerialNumberData StorageGetMediaSerialNumber(this IoControl IoControl)
        {
            if (!StorageGetMediaSerialNumber(IoControl, out var serialnumber))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return serialnumber;
        }
        /// <summary>
        /// IOCTL_STORAGE_RESET_BUS IOCTL
        /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ni-ntddstor-ioctl_storage_reset_bus
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static bool StorageResetBus(this IoControl IoControl, in StorageBusResetRequest request) => IoControl.DeviceIoControlInOnly(IOControlCode.StorageResetBus, request, out _);
        /// <summary>
        /// IOCTL_STORAGE_RESET_BUS IOCTL
        /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ni-ntddstor-ioctl_storage_reset_bus
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="PathId"></param>
        /// <returns></returns>
        public static bool StorageResetBus(this IoControl IoControl, byte PathId) => StorageResetBus(IoControl, (StorageBusResetRequest)PathId);
        /// <summary>
        /// OBSOLETE_IOCTL_STORAGE_RESET_BUS IOCTL
        /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ni-ntddstor-ioctl_storage_reset_bus
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static bool StorageObsoleteResetBus(this IoControl IoControl, in StorageBusResetRequest request) => IoControl.DeviceIoControlInOnly(IOControlCode.StorageObsoleteResetBus, request, out _);
        /// <summary>
        /// OBSOLETE_IOCTL_STORAGE_RESET_BUS IOCTL
        /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ni-ntddstor-ioctl_storage_reset_bus
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="PathId"></param>
        /// <returns></returns>
        public static bool StorageObsoleteResetBus(this IoControl IoControl, byte PathId) => StorageObsoleteResetBus(IoControl, (StorageBusResetRequest)PathId);
        /// <summary>
        /// IOCTL_STORAGE_BREAK_RESERVATION IOCTL
        /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ni-ntddstor-ioctl_storage_break_reservation
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        internal static bool StorageBreakReservation(this IoControl IoControl, in StorageBreakReservationRequest request) => IoControl.DeviceIoControlInOnly(IOControlCode.StorageBreakReservation, request, out _);
        /// <summary>
        /// IOCTL_STORAGE_BREAK_RESERVATION IOCTL
        /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ni-ntddstor-ioctl_storage_break_reservation
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="PathId"></param>
        /// <param name="TargetId"></param>
        /// <param name="Lun"></param>
        /// <returns></returns>
        public static bool StorageBreakReservation(this IoControl IoControl, byte PathId, byte TargetId, byte Lun) => StorageBreakReservation(IoControl, (PathId, TargetId, Lun));
        /// <summary>
        /// IOCTL_STORAGE_RESET_DEVICE IOCTL
        /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ni-ntddstor-ioctl_storage_reset_device
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool StorageResetDevice(this IoControl IoControl) => IoControl.DeviceIoControl(IOControlCode.StorageResetDevice, out _);
        /// <summary>
        /// OBSOLETE_IOCTL_STORAGE_RESET_DEVICE IOCTL
        /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ni-ntddstor-ioctl_storage_reset_device
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool StorageObsoleteResetDevice(this IoControl IoControl) => IoControl.DeviceIoControl(IOControlCode.StorageObsoleteResetDevice, out _);
    }

}
