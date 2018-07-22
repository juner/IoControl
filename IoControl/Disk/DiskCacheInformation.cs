using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DISK_CACHE_INFORMATION structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_disk_cache_information )
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public struct DiskCacheInformation
    {
        [FieldOffset(0)]
        public bool ParametersSavable;
        [FieldOffset(1)]
        public bool ReadCacheEnabled;
        [FieldOffset(2)]
        public bool WriteCacheEnabled;
        [FieldOffset(4)]
        public DiskCacheRetentionPriority ReadRetentionPriority;
        [FieldOffset(8)]
        public DiskCacheRetentionPriority WriteRetentionPriority;
        [FieldOffset(12)]
        public ushort DisablePrefetchTransferLength;
        [FieldOffset(14)]
        public bool PrefetchScalar;
        [FieldOffset(16)]
        private DiskCacheInformationScalarPrefetch ScalarPrefetch;
        [FieldOffset(16)]
        public DiskCacheInformationBlockPrefetch BlockPrefetch;
        public DiskCacheInformation(bool ParametersSavable, bool ReadCacheEnabled, bool WriteCacheEnabled, DiskCacheRetentionPriority ReadRetentionPriority, DiskCacheRetentionPriority WriteRetentionPriority, ushort DisablePrefetchTransferLength, DiskCacheInformationScalarPrefetch ScalarPrefetch): this()
            => (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, PrefetchScalar ,this.ScalarPrefetch) = (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, true, ScalarPrefetch);
        public DiskCacheInformation(bool ParametersSavable, bool ReadCacheEnabled, bool WriteCacheEnabled, DiskCacheRetentionPriority ReadRetentionPriority, DiskCacheRetentionPriority WriteRetentionPriority, ushort DisablePrefetchTransferLength, DiskCacheInformationBlockPrefetch BlockPrefetch): this()
            => (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, PrefetchScalar, this.BlockPrefetch) = (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, false, BlockPrefetch);
        public void Deconstruct(out bool ParametersSavable, out bool ReadCacheEnabled, out bool WriteCacheEnabled, out DiskCacheRetentionPriority ReadRetentionPriority, out DiskCacheRetentionPriority WriteRetentionPriority, out ushort DisablePrefetchTransferLength, out bool PrefetchScalar)
            => (ParametersSavable, ReadCacheEnabled, WriteCacheEnabled, ReadRetentionPriority, WriteRetentionPriority, DisablePrefetchTransferLength, PrefetchScalar) = (this.ParametersSavable, this.ReadCacheEnabled, this.WriteCacheEnabled, this.ReadRetentionPriority, this.WriteRetentionPriority, this.DisablePrefetchTransferLength, this.PrefetchScalar);
        public override string ToString()
            => $"{nameof(DiskCacheInformation)}{{{nameof(ParametersSavable)}:{ParametersSavable}, {nameof(ReadCacheEnabled)}:{ReadCacheEnabled}, {nameof(WriteCacheEnabled)}:{WriteCacheEnabled}, {nameof(ReadRetentionPriority)}:{ReadRetentionPriority}, {nameof(WriteRetentionPriority)}:{WriteRetentionPriority}, {nameof(DisablePrefetchTransferLength)}:{DisablePrefetchTransferLength}, {nameof(PrefetchScalar)}:{PrefetchScalar}, {(PrefetchScalar ? $"{nameof(ScalarPrefetch)}:{ScalarPrefetch}" : $"{nameof(BlockPrefetch)}:{BlockPrefetch}")}}}";
    }

    public enum DiskCacheRetentionPriority : int
    {
        EqualPriority,
        KeepPrefetchedData,
        KeepReadData
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskCacheInformationBlockPrefetch
    {
        public ushort Minimum;
        public ushort Maximum;
        public DiskCacheInformationBlockPrefetch(ushort Minimum, ushort Maximum)
            => (this.Minimum, this.Maximum) = (Minimum, Maximum);
        public void Deconstruct(out ushort Minimum, out ushort Maximum)
            => (Minimum, Maximum) = (this.Minimum, this.Maximum);
        public override string ToString()
            => $"{nameof(DiskCacheInformationBlockPrefetch)}{{{nameof(Minimum)}:{Minimum}, {nameof(Maximum)}:{Maximum}}}";
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DiskCacheInformationScalarPrefetch
    {
        public ushort Minimum;
        public ushort Maximum;
        public ushort MaximumBlocks;
        public DiskCacheInformationScalarPrefetch(ushort Minimum, ushort Maximum, ushort MaximumBlocks)
            => (this.Minimum, this.Maximum, this.MaximumBlocks) = (Minimum, Maximum, MaximumBlocks);
        public void Deconstruct(out ushort Minimum, out ushort Maximum, out ushort MaximumBlocks)
            => (Minimum, Maximum, MaximumBlocks) = (this.Minimum, this.Maximum, this.MaximumBlocks);
        public override string ToString()
            => $"{nameof(DiskCacheInformationScalarPrefetch)}{{{nameof(Minimum)}:{Minimum}, {nameof(Maximum)}:{Maximum}, {nameof(MaximumBlocks)}:{MaximumBlocks}}}";
    }
}
