using System.Runtime.InteropServices;

namespace IoControl.MassStorage
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct StorageAccessAlignmentDescriptor
    {
        public readonly uint Version;
        public readonly uint Size;
        public readonly uint BytesPerCacheLine;
        public readonly uint BytesOffsetForCacheAligment;
        public readonly uint BytesPerLogicalSector;
        public readonly uint BytesPerPhysicalSector;
        public readonly uint BytesOffsetForSectorAligment;
    }
}