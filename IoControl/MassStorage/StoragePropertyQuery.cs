using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
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
