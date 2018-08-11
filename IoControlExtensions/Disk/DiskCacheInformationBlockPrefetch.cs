using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// _DISK_CACHE_INFORMATION structure innser struct ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ns-winioctl-_disk_cache_information )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskCacheInformationBlockPrefetch
    {
        /// <summary>
        /// The minimum amount of data that can be prefetched into the cache on a disk operation, as an absolute number of disk blocks. This member is valid only when PrefetchScalar is FALSE.
        /// </summary>
        public ushort Minimum;
        /// <summary>
        /// The maximum amount of data that can be prefetched into the cache on a disk operation, as an absolute number of disk blocks. This member is valid only when PrefetchScalar is FALSE.
        /// </summary>
        public ushort Maximum;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Minimum"></param>
        /// <param name="Maximum"></param>
        public DiskCacheInformationBlockPrefetch(ushort Minimum, ushort Maximum)
            => (this.Minimum, this.Maximum) = (Minimum, Maximum);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Minimum"></param>
        /// <param name="Maximum"></param>
        public void Deconstruct(out ushort Minimum, out ushort Maximum)
            => (Minimum, Maximum) = (this.Minimum, this.Maximum);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(DiskCacheInformationBlockPrefetch)}{{{nameof(Minimum)}:{Minimum}, {nameof(Maximum)}:{Maximum}}}";
    }
}
