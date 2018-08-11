using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_PROPERTY_QUERY structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_storage_property_query )
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public readonly struct StoragePropertyQuery
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly StoragePropertyId PropertyId;
        /// <summary>
        /// 
        /// </summary>
        public readonly StorageQueryType QueryType;
        /// <summary>
        /// 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public readonly byte[] AdditionalParameters;
        public StoragePropertyQuery(StoragePropertyId PropertyId, StorageQueryType QueryType, byte[] AdditionalParameters)
            => (this.PropertyId, this.QueryType, this.AdditionalParameters) = (PropertyId, QueryType, AdditionalParameters ?? new byte[1]);
        public override string ToString()
            => $"{nameof(StoragePropertyQuery)}{{{nameof(PropertyId)}:{PropertyId},{nameof(QueryType)}:{QueryType},[{string.Join(" ", (AdditionalParameters ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";
        public static implicit operator StoragePropertyQuery((StoragePropertyId PropertyId, StorageQueryType QueryType, byte[] AdditionalParameters) self) => new StoragePropertyQuery(self.PropertyId, self.QueryType, self.AdditionalParameters);
    }
}
