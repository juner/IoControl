using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public readonly struct DiskCacheInformation
    {
        [FieldOffset(0)]
        public readonly bool ParametersSavable;
        [FieldOffset(1)]
        public readonly bool ReadCacheEnabled;
        [FieldOffset(2)]
        public readonly bool WriteCacheEnabled;
        [FieldOffset(4)]
        public readonly DiskCacheRetentionPriority ReadRetentionPriority;
        [FieldOffset(8)]
        public readonly DiskCacheRetentionPriority WriteRetentionPriority;
        [FieldOffset(12)]
        public readonly ushort DisablePrefetchTransferLength;
        [FieldOffset(14)]
        public readonly bool PrefetchScalar;
        [FieldOffset(16)]
        public readonly DiskCacheInformationScalarPrefetch ScalarPrefetch;
        [FieldOffset(16)]
        public readonly DiskCacheInformationBlockPrefetch BlockPrefetch;

        public DiskCacheInformation(bool ParametersSavable, bool ReadCacheEnabled, bool WriteCacheEnabled, DiskCacheRetentionPriority ReadRetentionPriority, DiskCacheRetentionPriority WriteRetentionPriority, ushort DisablePrefetchTransferLength, bool PrefetchScalar, DiskCacheInformationScalarPrefetch? ScalarPrefetch = default, DiskCacheInformationBlockPrefetch? BlockPrefetch = default)
        {
            (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, this.PrefetchScalar)
            = (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, PrefetchScalar);
            if (this.PrefetchScalar)
            {
                this.BlockPrefetch = default;
                this.ScalarPrefetch = ScalarPrefetch.Value;
            }
            else
            {
                this.ScalarPrefetch = default;
                this.BlockPrefetch = BlockPrefetch.Value;
            }
        }
        public DiskCacheInformation(bool ParametersSavable, bool ReadCacheEnabled, bool WriteCacheEnabled, DiskCacheRetentionPriority ReadRetentionPriority, DiskCacheRetentionPriority WriteRetentionPriority, ushort DisablePrefetchTransferLength, DiskCacheInformationScalarPrefetch ScalarPrefetch) 
            : this()
            => (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, PrefetchScalar, this.ScalarPrefetch)
            = (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, true, ScalarPrefetch);
        public void Deconstruct(out bool ParametersSavable, out bool ReadCacheEnabled, out bool WriteCacheEnabled, out DiskCacheRetentionPriority ReadRetentionPriority, out DiskCacheRetentionPriority WriteRetentionPriority, out ushort DisablePrefetchTransferLength, out bool PrefetchScalar, out DiskCacheInformationScalarPrefetch ScalarPrefetch)
            => (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, PrefetchScalar, ScalarPrefetch)
            = (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, this.PrefetchScalar, this.ScalarPrefetch);
        public void Deconstruct(out bool ParametersSavable, out bool ReadCacheEnabled, out bool WriteCacheEnabled, out DiskCacheRetentionPriority ReadRetentionPriority, out DiskCacheRetentionPriority WriteRetentionPriority, out ushort DisablePrefetchTransferLength, out bool PrefetchScalar, out DiskCacheInformationBlockPrefetch BlockPrefetch)
            => (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, PrefetchScalar, BlockPrefetch)
            = (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, this.PrefetchScalar, this.BlockPrefetch);
        public void Deconstruct(out bool ParametersSavable, out bool ReadCacheEnabled, out bool WriteCacheEnabled, out DiskCacheRetentionPriority ReadRetentionPriority, out DiskCacheRetentionPriority WriteRetentionPriority, out ushort DisablePrefetchTransferLength, out bool PrefetchScalar, out DiskCacheInformationScalarPrefetch ScalarPrefetch, out DiskCacheInformationBlockPrefetch BlockPrefetch)
            => (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, PrefetchScalar, ScalarPrefetch, BlockPrefetch)
            = (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, this.PrefetchScalar, this.PrefetchScalar ? this.ScalarPrefetch : default, !this.PrefetchScalar ? this.BlockPrefetch : default);
        public override string ToString()
            => $"{nameof(DiskCacheInformation)}{{{nameof(ParametersSavable)}:{ParametersSavable}, {nameof(ReadCacheEnabled)}:{ReadCacheEnabled}, {nameof(WriteCacheEnabled)}:{WriteCacheEnabled}, {nameof(ReadRetentionPriority)}:{ReadRetentionPriority}, {nameof(WriteRetentionPriority)}:{WriteRetentionPriority}, {nameof(DisablePrefetchTransferLength)}:{DisablePrefetchTransferLength}, {nameof(PrefetchScalar)}:{PrefetchScalar}, {(PrefetchScalar ? $"{nameof(ScalarPrefetch)}:{ScalarPrefetch}" : $"{nameof(BlockPrefetch)}:{BlockPrefetch}")}}}";
    }
}
