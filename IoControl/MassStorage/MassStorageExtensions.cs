using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static IoControl.IoControl;

namespace IoControl.MassStorage
{
    public static class MassStorageExtensions
    {
        public static void StorageGetDeviceNumber(this IoControl IoControl, out StorageDeviceNumber number)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetDeviceNumber, out number, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static StorageDeviceNumber StorageGetDeviceNumber(this IoControl IoControl)
        {
            StorageGetDeviceNumber(IoControl, out var number);
            return number;
        }
        public static void StorageQueryProperty(this IoControl IoControl, StoragePropertyId PropertyId, StorageQueryType QueryType, byte[] AdditionalParameters, out StorageDeviceDescriptor descriptor)
        {
            var query = new StoragePropertyQuery {
                PropertyId = PropertyId,
                QueryType = QueryType,
                AdditionalParameters = AdditionalParameters ?? new byte[1],
            };
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
        public static void StorageGetMediaSerialNumber(this IoControl IoControl, out MediaSerialNumberData serialnumberdata)
        {
            var result = IoControl.DeviceIoControlOutOnly(IOControlCode.StorageGetMediaSerialNumber, out serialnumberdata, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        public static MediaSerialNumberData StorageGetMediaSerialNumber(this IoControl IoControl)
        {
            StorageGetMediaSerialNumber(IoControl, out var serialnumber);
            return serialnumber;
        }
    }
    /// <summary>
    /// MEDIA_SERIAL_NUMBER_DATA structure ( https://msdn.microsoft.com/library/windows/hardware/ff562213 )
    /// </summary>
    public readonly struct MediaSerialNumberData
    {
        public readonly uint SerialNumberLength;
        public readonly uint Result;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =2)]
        public readonly uint[] Reserved;
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 1)]
        public readonly byte[] SerialNumberData;

        MediaSerialNumberData(uint SerialNumberLength, uint Result, uint[] Reserved, byte[] SerialNumberData)
            => (this.SerialNumberLength, this.Result, this.Reserved, this.SerialNumberData) = (SerialNumberLength, Result, Reserved, SerialNumberData);
        public MediaSerialNumberData(uint SerialNumberLength, uint Result, byte[] SerialNumberData) : this(SerialNumberLength, Result, new uint[2], SerialNumberData) { }
        public MediaSerialNumberData(uint Result, byte[] SerialNumberData) : this((uint)SerialNumberData.Length, Result, new uint[2], SerialNumberData) { }
    }

}
