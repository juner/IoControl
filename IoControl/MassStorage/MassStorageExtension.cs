using System;
using System.Collections.Generic;
using System.Linq;
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

    [StructLayout(LayoutKind.Sequential)]
    public struct StorageDeviceNumber
    {
        public FileDevice DeviceType;
        public uint DeviceNumber;
        public uint PartitionNumber;
        public override string ToString()
            => $"{nameof(StorageDeviceNumber)}{{{nameof(DeviceType)}:{DeviceType}, {nameof(DeviceNumber)}:{DeviceNumber}, {nameof(PartitionNumber)}:{PartitionNumber}}}";
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct StoragePropertyQuery
    {
        public StoragePropertyId PropertyId;
        public StorageQueryType QueryType;
        [MarshalAs(UnmanagedType.ByValArray)]
        public byte[] AdditionalParameters;
        public override string ToString()
            => $"{nameof(StoragePropertyQuery)}{{{nameof(PropertyId)}:{PropertyId},{nameof(QueryType)}:{QueryType},[{string.Join(" ", (AdditionalParameters ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";
    }
    public enum StoragePropertyId : uint
    {
        StorageDeviceProperty = 0,
        StorageAdapterProperty = 1,
        StorageDeviceIdProperty = 2,
        StorageDeviceUniqueIdProperty = 3,
        StorageDeviceWriteCacheProperty = 4,
        StorageMiniportProperty = 5,
        StorageAccessAlignmentProperty = 6,
        StorageDeviceSeekPenaltyProperty = 7,
        StorageDeviceTrimProperty = 8,
        StorageDeviceWriteAggregationProperty = 9,
        StorageDeviceDeviceTelemetryProperty = 10, // 0xA
        StorageDeviceLBProvisioningProperty = 11, // 0xB
        StorageDevicePowerProperty = 12, // 0xC
        StorageDeviceCopyOffloadProperty = 13, // 0xD
        StorageDeviceResiliencyProperty = 14 // 0xE
    }
    public enum StorageQueryType : uint
    {
        /// <summary>Instructs the driver to return an appropriate descriptor.</summary>
        StandardQuery = 0,
        /// <summary>Instructs the driver to report whether the descriptor is supported.</summary>
        ExistsQuery = 1,
        /// <summary>Used to retrieve a mask of writeable fields in the descriptor. Not currently supported. Do not use.</summary>
        MaskQuery = 2,
        /// <summary>Specifies the upper limit of the list of query types. This is used to validate the query type.</summary>
        QueryMaxDefined = 3
    }
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_device_seek_penalty_descriptor
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DeviceSeekPenaltyDescriptor
    {
        public readonly uint Version;
        public readonly uint Size;
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool IncursSeekPenalty;
        public DeviceSeekPenaltyDescriptor(uint Version, uint Size, bool IncursSeekPenalty)
            => (this.Version, this.Size, this.IncursSeekPenalty) = (Version, Size, IncursSeekPenalty);
        public void Deconstruct(out uint Version, out uint Size, out bool IncursSeekPenalty)
            => (Version, Size, IncursSeekPenalty) = (this.Version, this.Size, this.IncursSeekPenalty);
        public override string ToString()
            => $"{nameof(DeviceSeekPenaltyDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(IncursSeekPenalty)}:{IncursSeekPenalty}}}";
    }
}
