using System;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// CREATE_DISK_GPT structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_create_disk_gpt )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CreateDiskGpt
    {
        public Guid DiskId;
        public uint MaxPartitionCount;
        public override string ToString()
            => $"{nameof(CreateDiskGpt)}{{{nameof(DiskId)}:{DiskId}, {nameof(MaxPartitionCount)}:{MaxPartitionCount}}}";
    }
}
