using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DISK_PARTITION_INFO Structure ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ns-winioctl-_disk_partition_info )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DiskPartitionInfo
    {
        /// <summary>
        /// The size of this structure, in bytes.
        /// </summary>
        public readonly uint SizeOfPertitionInfo;
        /// <summary>
        /// The format of a partition.
        /// </summary>
        public readonly PartitionStyle PartitionStyle;
        private readonly DiskPertitionInfoUnion Info;
        public DiskPartitionInfoMbr Mbr => Info.Mbr;
        public DiskPartitionInfoGpt Gpt => Info.Gpt;

        [StructLayout(LayoutKind.Explicit)]
        private readonly struct DiskPertitionInfoUnion
        {
            [FieldOffset(0)]
            public readonly DiskPartitionInfoMbr Mbr;
            [FieldOffset(0)]
            public readonly DiskPartitionInfoGpt Gpt;
        }
    }
}
