using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static IoControl.IoControl;
using static IoControl.PtrUtils;

namespace IoControl.MassStorage
{
    public static class MassStorageExtensions
    {
        /// <summary>
        /// IOCTL_STORAGE_CHECK_VERIFY IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool StorageCheckVerify(this IoControl IoControl) => IoControl.DeviceIoControl(IOControlCode.StorageCheckVerify, out _);
        /// <summary>
        /// IOCTL_STORAGE_CHECK_VERIFY2 IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool StorageCheckVerify2(this IoControl IoControl) => IoControl.DeviceIoControl(IOControlCode.StorageCheckVerify2, out _);

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
        /// <summary>
        /// IOCTL_STORAGE_GET_DEVICE_NUMBER_EX IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="number"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public static bool StorageGetDeviceNumberEx(this IoControl IoControl, out StorageDeviceNumberEx number, out uint ReturnBytes) => IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetDeviceNumberEx, out number, out ReturnBytes);
        /// <summary>
        /// IOCTL_STORAGE_GET_DEVICE_NUMBER_EX IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static StorageDeviceNumberEx StorageGetDeviceNumberEx(this IoControl IoControl)
        {
            if (!StorageGetDeviceNumberEx(IoControl, out var number, out var ReturnBytes) && ReturnBytes == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return number;
        }
        public static bool StorageQueryProperty(this IoControl IoControl, StoragePropertyId PropertyId, StorageQueryType QueryType, byte[] AdditionalParameters, out IStorageDescriptor descriptor, out uint ReturnBytes)
        {
            var genericType = PropertyId.GetDestType() ?? typeof(StorageDescriptor);
            if (genericType == null)
                throw new ArgumentException($"{nameof(PropertyId)} is not have {nameof(StoragePropertyAttribute)}.{nameof(StoragePropertyAttribute.DestType)}");
            var query = new StoragePropertyQuery(PropertyId, QueryType, AdditionalParameters);
            var argument = new object[] { IoControl, query, null, null };
            var result = (bool)typeof(MassStorageExtensions)
                .GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .FirstOrDefault(mi => mi.Name == nameof(StorageQueryProperty) && mi.IsGenericMethodDefinition)
                .MakeGenericMethod(genericType)
                .Invoke(null, argument);
            descriptor = (IStorageDescriptor)argument[2];
            ReturnBytes = (uint)argument[3];
            return result;
            
        }
        public static IStorageDescriptor StorageQueryProperty(this IoControl IoControl, StoragePropertyId PropertyId, StorageQueryType QueryType = default, params byte[] AdditionalParameters)
        {
            if (!StorageQueryProperty(IoControl, PropertyId, QueryType, AdditionalParameters, out var descriptor, out var ReturnBytes) && ReturnBytes == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return descriptor;
        }
        public static bool StorageQueryProperty<T>(this IoControl IoControl, in StoragePropertyQuery query, out T descriptor, out uint ReturnBytes)
            where T: struct, IStorageDescriptor
        {
            using (query.CreatePtr(out var inPtr, out var inSize))
            {
                uint outSize;
                using (CreatePtr<StorageDescriptorHeader>(out var headerPtr, out var headerSize))
                {
                    var result = IoControl.DeviceIoControl(IOControlCode.StorageQueryProperty, inPtr, inSize, headerPtr, headerSize, out ReturnBytes);
                    if (!result)
                    {
                        descriptor = default;
                        return result;
                    }
                    outSize = new StorageDescriptorHeader(headerPtr, ReturnBytes).Size;
                }
                using (CreatePtr(outSize, out var outPtr))
                {
                    var result = IoControl.DeviceIoControl(IOControlCode.StorageQueryProperty, inPtr, inSize, outPtr, outSize, out ReturnBytes);
                    descriptor = PtrToStructure<T>(outPtr, ReturnBytes);
                    return result;
                }
            }
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
        public static bool StorageBreakReservation(this IoControl IoControl, byte PathId, byte TargetId, byte Lun) => StorageBreakReservation(IoControl, new StorageBreakReservationRequest(PathId, TargetId, Lun));
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
        /// <summary>
        /// IOCTL_STORAGE_MEDIA_REMOVAL IOCTL
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="PreventMediaRemoval"><see cref="true"/> means prevent media from being removed. <see cref="false"/> means allow media removal.</param>
        /// <returns></returns>
        public static bool StorageMediaRemoval(this IoControl IoControl, bool PreventMediaRemoval) => IoControl.DeviceIoControlInOnly(IOControlCode.StorageMediaRemoval, new PreventMediaRemoval(PreventMediaRemoval), out _);
        /// <summary>
        /// IOCTL_STORAGE_GET_MEDIA_TYPES
        /// </summary>
        /// <param name="IoControl"></param>
        /// <param name="MediaTypes"></param>
        /// <param name="ReturnBytes"></param>
        /// <returns></returns>
        public static bool StorageGetMediaTypes(this IoControl IoControl, out Disk.DiskGeometry[] MediaTypes, out uint ReturnBytes)
        {
            var BaseSize = (uint)Marshal.SizeOf<Disk.DiskGeometry>() * 8u;
            var Size = BaseSize;
            var ReturnSize = 0u;
            while (ReturnSize == 0u)
                using (CreatePtr(Size, out var Ptr))
                {
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetMediaTypes, Ptr, Size, out ReturnSize);
                    if (result)
                    {
                        MediaTypes = Enumerable.Range(0, (int)(ReturnSize / BaseSize))
                            .Select(index => PtrToStructure<Disk.DiskGeometry>(IntPtr.Add(Ptr, (int)BaseSize * index), BaseSize))
                            .ToArray();
                        ReturnBytes = ReturnSize;
                        return result;
                    }
                    var ErrorCode = Marshal.GetHRForLastWin32Error();
                    if (ErrorCode == ERROR_INSUFFICIENT_BUFFER)
                    {
                        Size *= 2;
                        continue;
                    }
                    break;
                }
            ReturnBytes = ReturnSize;
            MediaTypes = null;
            return false;
        }
        /// <summary>
        /// IOCTL_STORAGE_GET_MEDIA_TYPES
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static Disk.DiskGeometry[] StorageGetMediaTypes(this IoControl IoControl)
        {
            if (!StorageGetMediaTypes(IoControl, out var MediaTypes, out var ReturnBytes) && ReturnBytes == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return MediaTypes;
        }
        public static bool StorageGetMediaTypesEx(this IoControl IoControl, out GetMediaTypes MediaTypes, out uint ReturnBytes)
        {
            var BaseSize = (uint)Marshal.SizeOf<GetMediaTypes>() * 8u;
            var Size = BaseSize;
            var ReturnSize = 0u;
            while (ReturnSize == 0u)
                using (CreatePtr(Size, out var Ptr))
                {
                    var result = IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetMediaTypesEx, Ptr, Size, out ReturnSize);
                    if (result)
                    {
                        MediaTypes = GetMediaTypes.FromPtr(Ptr, ReturnSize);
                        ReturnBytes = ReturnSize;
                        return result;
                    }
                    var ErrorCode = Marshal.GetHRForLastWin32Error();
                    if (ErrorCode == ERROR_INSUFFICIENT_BUFFER)
                    {
                        Size *= 2;
                        continue;
                    }
                    break;
                }
            ReturnBytes = ReturnSize;
            MediaTypes = default;
            return false;
        }
        public static GetMediaTypes StorageGetMediaTypesEx(this IoControl IoControl)
        {
            if (!StorageGetMediaTypesEx(IoControl, out var MediaTypes, out var ReturnBytes) && ReturnBytes == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return MediaTypes;
        }
    }
}
