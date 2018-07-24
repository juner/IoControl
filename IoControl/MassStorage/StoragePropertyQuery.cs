using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_PROPERTY_QUERY structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_storage_property_query )
    /// </summary>
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
}
