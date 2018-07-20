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
        public static void StorageQueryProperty(this IoControl IoControl, StoragePropertyId PropertyId, StorageQueryType QueryType, byte[] AdditionalParameters, out DeviceSeekPenaltyDescriptor descriptor)
        {
            var query = new StoragePropertyQuery {
                PropertyId = PropertyId,
                QueryType = QueryType,
                AdditionalParameters = AdditionalParameters ?? new byte[1],
            };
            StorageQueryProperty(IoControl, ref query, out descriptor);
        }
        public static DeviceSeekPenaltyDescriptor StorageQueryProperty(this IoControl IoControl, StoragePropertyId PropertyId, StorageQueryType QueryType = default, params byte[] AdditionalParameters)
        {
            StorageQueryProperty(IoControl, PropertyId, QueryType, AdditionalParameters, out var descriptor);
            return descriptor;
        }
        public static void StorageQueryProperty(this IoControl IoControl, ref StoragePropertyQuery query, out DeviceSeekPenaltyDescriptor descriptor)
        {
            var Pack = typeof(StoragePropertyQuery).StructLayoutAttribute.Pack;
            const int propertyIdSize = sizeof(StoragePropertyId);
            const int queryTypeSize = sizeof(StorageQueryType);
            int additionalSize = sizeof(byte) * (query.AdditionalParameters?.Length + 1) ?? 1;
            uint inSize = (uint)(propertyIdSize + queryTypeSize + additionalSize);
            if (inSize % Pack > 0) inSize = (uint)(int)(Math.Ceiling(inSize / (double)Pack) * Pack);
            var outSize = (uint)Marshal.SizeOf(typeof(DeviceSeekPenaltyDescriptor));
            var inPtr = Marshal.AllocCoTaskMem((int)inSize);
            var outPtr = Marshal.AllocCoTaskMem((int)outSize);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(inPtr)))
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(outPtr)))
            {
                var _inPtr = inPtr;
                Marshal.WriteInt32(_inPtr, unchecked((int)query.PropertyId));
                _inPtr += propertyIdSize;
                Marshal.WriteInt32(_inPtr, unchecked((int)query.QueryType));
                _inPtr += queryTypeSize;
                foreach (var elm in query.AdditionalParameters) {
                    Marshal.WriteByte(_inPtr, elm);
                    _inPtr += sizeof(byte);
                }
                Marshal.WriteByte(_inPtr, unchecked((byte)-1));
                var result = IoControl.DeviceIoControl(IOControlCode.StorageQueryProperty, inPtr,inSize, outPtr, outSize, out var penalty_size);
                if (!result)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                descriptor = (DeviceSeekPenaltyDescriptor)Marshal.PtrToStructure(outPtr, typeof(DeviceSeekPenaltyDescriptor));
            }
        }
        public static DeviceSeekPenaltyDescriptor StorageQueryProperty(this IoControl IoControl, ref StoragePropertyQuery query)
        {
            StorageQueryProperty(IoControl, ref query, out var descriptor);
            return descriptor;
        }
    }
}
