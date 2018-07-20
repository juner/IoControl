using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    #region DiskCacheInformation
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
        public DiskCacheInformationScalarPrefetch ScalarPrefetch;
        [FieldOffset(16)]
        public DiskCacheInformationBlockPrefetch BlockPrefetch;
        public override string ToString()
            => $"{nameof(DiskCacheInformation)}{{{nameof(ParametersSavable)}:{ParametersSavable}, {nameof(ReadCacheEnabled)}:{ReadCacheEnabled}, {nameof(WriteCacheEnabled)}:{WriteCacheEnabled}, {nameof(ReadRetentionPriority)}:{ReadRetentionPriority}, {nameof(WriteRetentionPriority)}:{WriteRetentionPriority}, {nameof(DisablePrefetchTransferLength)}:{DisablePrefetchTransferLength}, {nameof(PrefetchScalar)}:{PrefetchScalar}, {(PrefetchScalar ? $"{nameof(ScalarPrefetch)}:{ScalarPrefetch}" : $"{nameof(BlockPrefetch)}:{BlockPrefetch}")}}}";
    }

#endregion
#region DriveLayoutInformation
#endregion
}
