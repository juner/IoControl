using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskCacheInformationBlockPrefetch
    {
        public ushort Minimum;
        public ushort Maximum;
        public override string ToString()
            => $"{nameof(DiskCacheInformationBlockPrefetch)}{{{nameof(Minimum)}:{Minimum}, {nameof(Maximum)}:{Maximum}}}";
    }
}
