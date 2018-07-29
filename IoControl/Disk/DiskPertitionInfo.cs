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
        public readonly uint SizeOfPartitionInfo;
        /// <summary>
        /// The format of a partition.
        /// </summary>
        public readonly PartitionStyle PartitionStyle;
        /// <summary>
        /// <see cref="DiskPartitionInfoMbr"/> or <see cref="DiskPartitionInfoGpt"/>
        /// </summary>
        private readonly DiskPartitionInfoUnion Info;
        /// <summary>
        /// Mbr 
        /// </summary>
        public DiskPartitionInfoMbr Mbr => Info;
        /// <summary>
        /// Gpt
        /// </summary>
        public DiskPartitionInfoGpt Gpt => Info;
        private DiskPartitionInfo(uint SizeOfPartitionInfo, PartitionStyle PartitionStyle, DiskPartitionInfoUnion Info)
            => (this.SizeOfPartitionInfo, this.PartitionStyle, this.Info) = (SizeOfPartitionInfo, PartitionStyle, Info);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SizeOfPartitionInfo"></param>
        public DiskPartitionInfo(uint SizeOfPartitionInfo) : this(SizeOfPartitionInfo, PartitionStyle.Raw, default) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SizeOfPartitionInfo"></param>
        /// <param name="Mbr"></param>
        public DiskPartitionInfo(uint SizeOfPartitionInfo, DiskPartitionInfoMbr Mbr) : this(SizeOfPartitionInfo, PartitionStyle.Mbr, Mbr) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SizeOfPartitionInfo"></param>
        /// <param name="Gpt"></param>
        public DiskPartitionInfo(uint SizeOfPartitionInfo, DiskPartitionInfoGpt Gpt) : this(SizeOfPartitionInfo, PartitionStyle.Gpt, Gpt) { }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(DiskPartitionInfo)}{{{nameof(SizeOfPartitionInfo)}:{SizeOfPartitionInfo}, {nameof(PartitionStyle)}:{PartitionStyle}{(PartitionStyle == PartitionStyle.Mbr ? $", {nameof(Mbr)}:{Mbr}" : PartitionStyle == PartitionStyle.Gpt ? $", {nameof(Gpt)}:{Gpt}" : "")}}}";
        [StructLayout(LayoutKind.Explicit)]
        private readonly struct DiskPartitionInfoUnion
        {
            [FieldOffset(0)]
            private readonly DiskPartitionInfoMbr Mbr;
            [FieldOffset(0)]
            private readonly DiskPartitionInfoGpt Gpt;
            private DiskPartitionInfoUnion(DiskPartitionInfoMbr Mbr) : this()
                => this.Mbr = Mbr;
            private DiskPartitionInfoUnion(DiskPartitionInfoGpt Gpt) : this()
                => this.Gpt = Gpt;
            public static implicit operator DiskPartitionInfoUnion(in DiskPartitionInfoMbr Mbr) => new DiskPartitionInfoUnion(Mbr);
            public static implicit operator DiskPartitionInfoMbr(in DiskPartitionInfoUnion Union) => Union.Mbr;
            public static implicit operator DiskPartitionInfoUnion(in DiskPartitionInfoGpt Gpt) => new DiskPartitionInfoUnion(Gpt);
            public static implicit operator DiskPartitionInfoGpt(in DiskPartitionInfoUnion Union) => Union.Gpt;
        }
    }
}
