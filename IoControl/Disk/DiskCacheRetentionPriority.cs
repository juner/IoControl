namespace IoControl.Disk
{
    public enum DiskCacheRetentionPriority : int
    {
        EqualPriority,
        KeepPrefetchedData,
        KeepReadData
    }
}
