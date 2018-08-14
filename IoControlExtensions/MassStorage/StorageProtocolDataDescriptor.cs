using System;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_PROTOCOL_DATA_DESCRIPTOR
    /// https://docs.microsoft.com/ja-jp/windows/desktop/api/winioctl/ns-winioctl-_storage_protocol_data_descriptor
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageProtocolDataDescriptor : IStorageDescriptor
    {
        uint IStorageDescriptor.Version => Version;
        uint IStorageDescriptor.Size => Size;
        /// <summary>
        /// The version of this structure.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// The total size of the descriptor, including the space for all protocol data.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// The protocol-specific data, of type <see cref="StorageProtocolSpecificData"/>.
        /// </summary>
        public readonly StorageProtocolSpecificData ProtocolSpecificData;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProtocolSpecificData"></param>
        public StorageProtocolDataDescriptor(StorageProtocolSpecificData ProtocolSpecificData)
            => (Version, Size, this.ProtocolSpecificData) = ((uint)Marshal.SizeOf<StorageProtocolDataDescriptor>(), (uint)Marshal.SizeOf<StorageProtocolDataDescriptor>(), ProtocolSpecificData);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        public StorageProtocolDataDescriptor(IntPtr IntPtr, uint Size) => this = (StorageProtocolDataDescriptor)Marshal.PtrToStructure(IntPtr, typeof(StorageProtocolDataDescriptor));
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(StorageProtocolDataDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(ProtocolSpecificData)}:{ProtocolSpecificData}}}";
    }
}