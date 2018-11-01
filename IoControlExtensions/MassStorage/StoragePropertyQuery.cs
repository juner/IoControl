using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_PROPERTY_QUERY structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntddstor/ns-ntddstor-_storage_property_query )
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public readonly struct StoragePropertyQuery : DataUtils.IPtrCreatable
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
        public StoragePropertyQuery(StoragePropertyId PropertyId, StorageQueryType QueryType = default, byte[] AdditionalParameters = null)
            => (this.PropertyId, this.QueryType, this.AdditionalParameters) = (PropertyId, QueryType, AdditionalParameters ?? new byte[1]);
        public override string ToString()
            => $"{nameof(StoragePropertyQuery)}{{{nameof(PropertyId)}:{PropertyId},{nameof(QueryType)}:{QueryType},[{string.Join(" ", (AdditionalParameters ?? Enumerable.Empty<byte>()).Select(v => $"{v:X2}"))}]}}";
        public static implicit operator StoragePropertyQuery((StoragePropertyId PropertyId, StorageQueryType QueryType, byte[] AdditionalParameters) self) => new StoragePropertyQuery(self.PropertyId, self.QueryType, self.AdditionalParameters);
        public IDisposable CreatePtr(out IntPtr IntPtr, out uint Size)
        {
            var Pack = typeof(StoragePropertyQuery).StructLayoutAttribute.Pack;
            const int propertyIdSize = sizeof(StoragePropertyId);
            const int queryTypeSize = sizeof(StorageQueryType);
            int additionalSize = sizeof(byte) * (AdditionalParameters?.Length + 1) ?? 1;
            Size = (uint)(propertyIdSize + queryTypeSize + additionalSize);
            if (Size % Pack > 0) Size = (uint)(int)(Math.Ceiling(Size / (double)Pack) * Pack);

            var inPtr = Marshal.AllocCoTaskMem((int)Size);
            var Dispose = Disposable.Create(() => Marshal.FreeCoTaskMem(inPtr));
            try
            {
                var _inPtr = inPtr;
                Marshal.WriteInt32(_inPtr, unchecked((int)PropertyId));
                _inPtr += propertyIdSize;
                Marshal.WriteInt32(_inPtr, unchecked((int)QueryType));
                _inPtr += queryTypeSize;
                foreach (var elm in AdditionalParameters)
                {
                    Marshal.WriteByte(_inPtr, elm);
                    _inPtr += sizeof(byte);
                }
                Marshal.WriteByte(_inPtr, unchecked((byte)-1));
                IntPtr = inPtr;
                return Dispose;
            }
            catch
            {
                Dispose.Dispose();
                throw;
            }
        }
    }
}
