using System;
using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    /// <summary>
    /// STORAGE_ACCESS_ALIGNMENT_DESCRIPTOR
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ns-winioctl-_storage_access_alignment_descriptor
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageAccessAlignmentDescriptor : IStorageDescriptor
    {
        uint IStorageDescriptor.Size => Size;
        uint IStorageDescriptor.Version => Version;
        /// <summary>
        /// Contains the size of this structure, in bytes. The value of this member will change as members are added to the structure.
        /// </summary>
        public readonly uint Version;
        /// <summary>
        /// Specifies the total size of the data returned, in bytes. This may include data that follows this structure.
        /// </summary>
        public readonly uint Size;
        /// <summary>
        /// The number of bytes in a cache line of the device.
        /// </summary>
        public readonly uint BytesPerCacheLine;
        /// <summary>
        /// The address offset necessary for proper cache access alignment, in bytes.
        /// </summary>
        public readonly uint BytesOffsetForCacheAligment;
        /// <summary>
        /// The number of bytes in a logical sector of the device.
        /// </summary>
        public readonly uint BytesPerLogicalSector;
        /// <summary>
        /// The number of bytes in a physical sector of the device.
        /// </summary>
        public readonly uint BytesPerPhysicalSector;
        /// <summary>
        /// The logical sector offset within the first physical sector where the first logical sector is placed, in bytes.
        /// </summary>
        public readonly uint BytesOffsetForSectorAligment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BytesPerCacheLine"></param>
        /// <param name="BytesOffsetForCacheAligment"></param>
        /// <param name="BytesPerLogicalSector"></param>
        /// <param name="BytesPerPhysicalSector"></param>
        /// <param name="BytesOffsetForSectorAligment"></param>
        public StorageAccessAlignmentDescriptor(uint BytesPerCacheLine, uint BytesOffsetForCacheAligment, uint BytesPerLogicalSector, uint BytesPerPhysicalSector, uint BytesOffsetForSectorAligment)
            => (Version, Size, this.BytesPerCacheLine, this.BytesOffsetForCacheAligment, this.BytesPerLogicalSector, this.BytesPerPhysicalSector, this.BytesOffsetForSectorAligment)
            = ((uint)Marshal.SizeOf<StorageAccessAlignmentDescriptor>(), (uint)Marshal.SizeOf<StorageAccessAlignmentDescriptor>(), BytesPerCacheLine, BytesOffsetForCacheAligment, BytesPerLogicalSector, BytesPerPhysicalSector, BytesOffsetForSectorAligment);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntPtr"></param>
        /// <param name="Size"></param>
        public StorageAccessAlignmentDescriptor(IntPtr IntPtr, uint Size) => this = (StorageAccessAlignmentDescriptor)Marshal.PtrToStructure(IntPtr, typeof(StorageAccessAlignmentDescriptor));
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(StorageAccessAlignmentDescriptor)}{{{nameof(Version)}:{Version}, {nameof(Size)}:{Size}, {nameof(BytesPerCacheLine)}:{BytesPerCacheLine}, {nameof(BytesOffsetForCacheAligment)}:{BytesOffsetForCacheAligment}, {nameof(BytesPerLogicalSector)}:{BytesPerLogicalSector}, {nameof(BytesPerPhysicalSector)}:{BytesPerPhysicalSector}, {nameof(BytesOffsetForSectorAligment)}:{BytesOffsetForSectorAligment}}}";
    }
}