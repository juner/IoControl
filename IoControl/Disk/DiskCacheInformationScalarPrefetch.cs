using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskCacheInformationScalarPrefetch
    {
        public ushort Minimum;
        public ushort Maximum;
        public ushort MaximumBlocks;
        public override string ToString()
            => $"{nameof(DiskCacheInformationScalarPrefetch)}{{{nameof(Minimum)}:{Minimum}, {nameof(Maximum)}:{Maximum}, {nameof(MaximumBlocks)}:{MaximumBlocks}}}";
    }
}
