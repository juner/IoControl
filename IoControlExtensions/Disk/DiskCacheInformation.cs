using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DISK_CACHE_INFORMATION structure ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ns-winioctl-_disk_cache_information )
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 24)]
    public readonly struct DiskCacheInformation
    {
        /// <summary>
        /// Indicates whether the device is capable of saving any parameters in nonvolatile storage.
        /// </summary>
        public readonly bool ParametersSavable;
        /// <summary>
        /// Indicates whether the read cache is enabled.
        /// </summary>
        public readonly bool ReadCacheEnabled;
        /// <summary>
        /// Indicates whether the write cache is enabled.
        /// </summary>
        public readonly bool WriteCacheEnabled;
        /// <summary>
        /// Determines the likelihood of data cached from a read operation remaining in the cache. This data might be given a different priority than data cached under other circumstances, such as from a prefetch operation.
        /// This member can be one of the following values from the <see cref="DiskCacheRetentionPriority"/> enumeration type.
        /// </summary>
        public readonly DiskCacheRetentionPriority ReadRetentionPriority;
        /// <summary>
        /// Determines the likelihood of data cached from a write operation remaining in the cache. This data might be given a different priority than data cached under other circumstances, such as from a prefetch operation.
        /// </summary>
        public readonly DiskCacheRetentionPriority WriteRetentionPriority;
        /// <summary>
        /// Disables prefetching. Prefetching might be disabled whenever the number of blocks requested exceeds the value in DisablePrefetchTransferLength. When zero, prefetching is disabled no matter what the size of the block request.
        /// </summary>
        public readonly ushort DisablePrefetchTransferLength;
        /// <summary>
        /// If this member is <see cref="true"/>, the union is a ScalarPrefetch structure. Otherwise, the union is a BlockPrefetch structure.
        /// </summary>
        public readonly bool PrefetchScalar;
        /// <summary>
        /// 
        /// </summary>
        private readonly DiskCacheInformationPrefetchUnion Prefetch;
        /// <summary>
        /// 
        /// </summary>
        public DiskCacheInformationScalarPrefetch ScalarPrefetch => Prefetch;
        /// <summary>
        /// 
        /// </summary>
        public DiskCacheInformationBlockPrefetch BlockPrefetch => Prefetch;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParametersSavable"></param>
        /// <param name="ReadCacheEnabled"></param>
        /// <param name="WriteCacheEnabled"></param>
        /// <param name="ReadRetentionPriority"></param>
        /// <param name="WriteRetentionPriority"></param>
        /// <param name="DisablePrefetchTransferLength"></param>
        /// <param name="PrefetchScalar"></param>
        /// <param name="ScalarPrefetch"></param>
        /// <param name="BlockPrefetch"></param>
        public DiskCacheInformation(bool ParametersSavable, bool ReadCacheEnabled, bool WriteCacheEnabled, DiskCacheRetentionPriority ReadRetentionPriority, DiskCacheRetentionPriority WriteRetentionPriority, ushort DisablePrefetchTransferLength, bool PrefetchScalar, DiskCacheInformationScalarPrefetch? ScalarPrefetch = default, DiskCacheInformationBlockPrefetch? BlockPrefetch = default)
            => (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, this.PrefetchScalar, Prefetch)
            = (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, PrefetchScalar, PrefetchScalar ? (DiskCacheInformationPrefetchUnion)ScalarPrefetch.Value : (DiskCacheInformationPrefetchUnion)BlockPrefetch.Value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParametersSavable"></param>
        /// <param name="ReadCacheEnabled"></param>
        /// <param name="WriteCacheEnabled"></param>
        /// <param name="ReadRetentionPriority"></param>
        /// <param name="WriteRetentionPriority"></param>
        /// <param name="DisablePrefetchTransferLength"></param>
        /// <param name="ScalarPrefetch"></param>
        public DiskCacheInformation(bool ParametersSavable, bool ReadCacheEnabled, bool WriteCacheEnabled, DiskCacheRetentionPriority ReadRetentionPriority, DiskCacheRetentionPriority WriteRetentionPriority, ushort DisablePrefetchTransferLength, DiskCacheInformationScalarPrefetch ScalarPrefetch)
            : this()
            => (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, PrefetchScalar, Prefetch)
            = (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, true, ScalarPrefetch);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParametersSavable"></param>
        /// <param name="ReadCacheEnabled"></param>
        /// <param name="WriteCacheEnabled"></param>
        /// <param name="ReadRetentionPriority"></param>
        /// <param name="WriteRetentionPriority"></param>
        /// <param name="DisablePrefetchTransferLength"></param>
        /// <param name="PrefetchScalar"></param>
        /// <param name="ScalarPrefetch"></param>
        public void Deconstruct(out bool ParametersSavable, out bool ReadCacheEnabled, out bool WriteCacheEnabled, out DiskCacheRetentionPriority ReadRetentionPriority, out DiskCacheRetentionPriority WriteRetentionPriority, out ushort DisablePrefetchTransferLength, out bool PrefetchScalar, out DiskCacheInformationScalarPrefetch ScalarPrefetch)
            => (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, PrefetchScalar, ScalarPrefetch)
            = (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, this.PrefetchScalar, Prefetch);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParametersSavable"></param>
        /// <param name="ReadCacheEnabled"></param>
        /// <param name="WriteCacheEnabled"></param>
        /// <param name="ReadRetentionPriority"></param>
        /// <param name="WriteRetentionPriority"></param>
        /// <param name="DisablePrefetchTransferLength"></param>
        /// <param name="PrefetchScalar"></param>
        /// <param name="BlockPrefetch"></param>
        public void Deconstruct(out bool ParametersSavable, out bool ReadCacheEnabled, out bool WriteCacheEnabled, out DiskCacheRetentionPriority ReadRetentionPriority, out DiskCacheRetentionPriority WriteRetentionPriority, out ushort DisablePrefetchTransferLength, out bool PrefetchScalar, out DiskCacheInformationBlockPrefetch BlockPrefetch)
            => (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, PrefetchScalar, BlockPrefetch)
            = (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, this.PrefetchScalar, Prefetch);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParametersSavable"></param>
        /// <param name="ReadCacheEnabled"></param>
        /// <param name="WriteCacheEnabled"></param>
        /// <param name="ReadRetentionPriority"></param>
        /// <param name="WriteRetentionPriority"></param>
        /// <param name="DisablePrefetchTransferLength"></param>
        /// <param name="PrefetchScalar"></param>
        /// <param name="ScalarPrefetch"></param>
        /// <param name="BlockPrefetch"></param>
        public void Deconstruct(out bool ParametersSavable, out bool ReadCacheEnabled, out bool WriteCacheEnabled, out DiskCacheRetentionPriority ReadRetentionPriority, out DiskCacheRetentionPriority WriteRetentionPriority, out ushort DisablePrefetchTransferLength, out bool PrefetchScalar, out DiskCacheInformationScalarPrefetch? ScalarPrefetch, out DiskCacheInformationBlockPrefetch? BlockPrefetch)
            => (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, PrefetchScalar, ScalarPrefetch, BlockPrefetch)
            = (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, this.PrefetchScalar, this.PrefetchScalar ? Prefetch : (DiskCacheInformationScalarPrefetch?)null, !this.PrefetchScalar ? Prefetch : (DiskCacheInformationBlockPrefetch?)null);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(DiskCacheInformation)}{{{nameof(ParametersSavable)}:{ParametersSavable}, {nameof(ReadCacheEnabled)}:{ReadCacheEnabled}, {nameof(WriteCacheEnabled)}:{WriteCacheEnabled}, {nameof(ReadRetentionPriority)}:{ReadRetentionPriority}, {nameof(WriteRetentionPriority)}:{WriteRetentionPriority}, {nameof(DisablePrefetchTransferLength)}:{DisablePrefetchTransferLength}, {nameof(PrefetchScalar)}:{PrefetchScalar}, {(PrefetchScalar ? $"{nameof(ScalarPrefetch)}:{ScalarPrefetch}" : $"{nameof(BlockPrefetch)}:{BlockPrefetch}")}}}";
        /// <summary>
        /// <see cref="DiskCacheInformationScalarPrefetch"/> or <see cref="DiskCacheInformationBlockPrefetch"/>
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        private struct DiskCacheInformationPrefetchUnion
        {
            [FieldOffset(0)]
            public readonly DiskCacheInformationScalarPrefetch ScalarPrefetch;
            [FieldOffset(0)]
            public readonly DiskCacheInformationBlockPrefetch BlockPrefetch;
            private DiskCacheInformationPrefetchUnion(DiskCacheInformationScalarPrefetch ScalarPrefetch) : this() => this.ScalarPrefetch = ScalarPrefetch;
            private DiskCacheInformationPrefetchUnion(DiskCacheInformationBlockPrefetch BlockPrefetch) : this() => this.BlockPrefetch = BlockPrefetch;
            public static implicit operator DiskCacheInformationScalarPrefetch(in DiskCacheInformationPrefetchUnion Union) => Union.ScalarPrefetch;
            public static implicit operator DiskCacheInformationPrefetchUnion(in DiskCacheInformationScalarPrefetch Scalar) => new DiskCacheInformationPrefetchUnion(Scalar);
            public static implicit operator DiskCacheInformationBlockPrefetch(in DiskCacheInformationPrefetchUnion Union) => Union.BlockPrefetch;
            public static implicit operator DiskCacheInformationPrefetchUnion(in DiskCacheInformationBlockPrefetch Block) => new DiskCacheInformationPrefetchUnion(Block);
        }
    }
}
