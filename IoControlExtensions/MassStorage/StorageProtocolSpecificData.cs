using System;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_PROTOCOL_SPECIFIC_DATA
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_storage_protocol_specific_data
    /// Describes protocol-specific device data, provided in the input and output buffer of an <see cref="IOControlCode.StorageQueryProperty"/> request.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageProtocolSpecificData
    {
        /// <summary>
        /// The protocol type. Values for this member are defined in the <see cref="StorageProtocolType"/> enumeration.
        /// </summary>
        public readonly StorageProtocolType ProtocolType;
        /// <summary>
        /// The protocol data type. Data types are defined in the <see cref="StorageProtocolNvmeDataType"/> and <see cref="StorageProtocolAtaDataType"/> enumerations.
        /// </summary>
        public readonly uint DataType;
        /// <summary>
        /// 
        /// </summary>
        public StorageProtocolNvmeDataType NvmeDataType => (StorageProtocolNvmeDataType)DataType;
        /// <summary>
        /// 
        /// </summary>
        public StorageProtocolAtaDataType AtaDataType => (StorageProtocolAtaDataType)DataType;
        /// <summary>
        /// The protocol data request value.
        /// </summary>
        public readonly uint ProtocolDataRequestValue;
        /// <summary>
        /// The sub value of the protocol data request.
        /// </summary>
        public readonly uint ProtocolDataRequestSubValue;
        /// <summary>
        /// The offset of the data buffer that is from the beginning of this structure. The typical value can be sizeof(STORAGE_PROTOCOL_SPECIFIC_DATA).
        /// </summary>
        public readonly uint ProtocolDataOffset;
        /// <summary>
        /// The length of the protocol data.
        /// </summary>
        public readonly uint ProtocolDataLength;
        /// <summary>
        /// The returned data.
        /// </summary>
        public readonly uint FixedProtocolReturnData;
        /// <summary>
        /// Reserved for future use.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public readonly uint[] Reserved;
        public StorageProtocolSpecificData(StorageProtocolType ProtocolType, uint DataType, uint ProtocolDataRequestValue, uint ProtocolDataRequestSubValue, uint ProtocolDataLength, uint FixedProtocolReturnData)
            => (this.ProtocolType, this.DataType, this.ProtocolDataRequestValue, this.ProtocolDataRequestSubValue, ProtocolDataOffset, this.ProtocolDataLength, this.FixedProtocolReturnData, Reserved)
            = (ProtocolType, DataType, ProtocolDataRequestValue, ProtocolDataRequestSubValue, (uint)Marshal.SizeOf<StorageProtocolSpecificData>(), ProtocolDataLength, FixedProtocolReturnData, new uint[3]);
        public StorageProtocolSpecificData(IntPtr IntPtr, uint Size) => this = (StorageProtocolSpecificData)Marshal.PtrToStructure(IntPtr, typeof(StorageProtocolSpecificData));
        
    }
    /// <summary>
    /// STORAGE_PROTOCOL_TYPE
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ne-ntddstor-_storage_protocol_type
    /// </summary>
    public enum StorageProtocolType : uint {
      Unknown      = 0 ,
      Scsi         ,
      Ata          ,
      Nvme         ,
      Sd           ,
      Ufs          ,
      Proprietary  ,
      MaxReserved
    }
    /// <summary>
    /// STORAGE_PROTOCOL_NVME_DATA_TYPE
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ne-ntddstor-_storage_protocol_nvme_data_type
    /// </summary>
    public enum StorageProtocolNvmeDataType
    {
        /// <summary>
        /// Unknown data type.
        /// </summary>
        Unknown,
        /// <summary>
        /// Identify data type. This can be either Identify Controller data or Identify Namespace data. When this type of data is being queried, the ProtocolDataRequestValue field of STORAGE_PROTOCOL_SPECIFIC_DATA will have a value of NVME_IDENTIFY_CNS_CONTROLLER for adapter or NVME_IDENTIFY_CNS_SPECIFIC_NAMESPACE for namespace. If the ProtocolDataRequestValue is NVME_IDENTIFY_CNS_SPECIFIC_NAMESPACE, the ProtocolDataRequestSubValue field from the STORAGE_PROTOCOL_SPECIFIC_DATA structure will have a value of the namespace ID.
        /// </summary>
        Identify,
        /// <summary>
        /// Log page data type.
        /// </summary>
        LogPage,
        /// <summary>
        /// Feature data type.
        /// </summary>
        Feature
    }
    /// <summary>
    /// STORAGE_PROTOCOL_ATA_DATA_TYPE
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ne-ntddstor-_storage_protocol_ata_data_type
    /// </summary>
    public enum StorageProtocolAtaDataType
    {
        /// <summary>
        /// Unknown data type.
        /// </summary>
        Unknown,
        /// <summary>
        /// Identify device data type.
        /// </summary>
        Identify,
        /// <summary>
        /// Log page data type.
        /// </summary>
        LogPage
    }
}