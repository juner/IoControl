using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DISK_CACHE_INFORMATION structure inner struct (https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ns-winioctl-_disk_cache_information)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskCacheInformationScalarPrefetch
    {
        /// <summary>
        /// The scalar multiplier of the transfer length of the request. This member is valid only when PrefetchScalar is TRUE. When PrefetchScalar is TRUE, this value is multiplied by the transfer length to obtain the minimum amount of data that can be prefetched into the cache on a disk operation.
        /// </summary>
        public ushort Minimum;
        /// <summary>
        /// The scalar multiplier of the transfer length of the request. This member is valid only when PrefetchScalar is TRUE. When PrefetchScalar is TRUE, this value is multiplied by the transfer length to obtain the maximum amount of data that can be prefetched into the cache on a disk operation.
        /// </summary>
        public ushort Maximum;
        /// <summary>
        /// The maximum number of blocks which can be prefetched.
        /// </summary>
        public ushort MaximumBlocks;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(DiskCacheInformationScalarPrefetch)}{{{nameof(Minimum)}:{Minimum}, {nameof(Maximum)}:{Maximum}, {nameof(MaximumBlocks)}:{MaximumBlocks}}}";
    }
}
