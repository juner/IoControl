using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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
            var result = IoControl.DeviceIoControl(IOControlCode.StorageQueryProperty, in query, out descriptor, out var penalty_size);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
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

    [StructLayout(LayoutKind.Sequential)]
    public struct StoragePropertyQuery
    {
        public StoragePropertyId PropertyId;
        public StorageQueryType QueryType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
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
        PropertyStandardQuery = 0,
        /// <summary>Instructs the driver to report whether the descriptor is supported.</summary>
        PropertyExistsQuery = 1,
        /// <summary>Used to retrieve a mask of writeable fields in the descriptor. Not currently supported. Do not use.</summary>
        PropertyMaskQuery = 2,
        /// <summary>Specifies the upper limit of the list of query types. This is used to validate the query type.</summary>
        PropertyQueryMaxDefined = 3
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct DeviceSeekPenaltyDescriptor
    {
        public uint Version;
        public uint Size;
        [MarshalAs(UnmanagedType.U1)]
        public bool IncursSeekPenalty;
        public override string ToString()
            => $"{nameof(DeviceSeekPenaltyDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(IncursSeekPenalty)}:{IncursSeekPenalty}}}";
    }
}
