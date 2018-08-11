using System.Runtime.InteropServices;
using static IoControl.Utils.ByteAndStructure;

namespace IoControl.Disk
{
    /// <summary>
    /// PARTITION_INFORMATION_EX structure ( https://docs.microsoft.com/en-us/windows/desktop/api/winioctl/ns-winioctl-_partition_information_ex )
    /// Contains partition information for standard AT-style master boot record (MBR) and Extensible Firmware Interface (EFI) disks.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct PartitionInformationEx
    {
        /// <summary>
        /// The format of the partition. For a list of values, see PARTITION_STYLE.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public readonly PartitionStyle PartitionStyle;
        /// <summary>
        /// The starting offset of the partition.
        /// </summary>
        public readonly long StartingOffset;
        /// <summary>
        /// The size of the partition, in bytes.
        /// </summary>
        public readonly long PartitionLength;
        /// <summary>
        /// The number of the partition (1-based).
        /// </summary>
        public readonly uint PartitionNumber;
        /// <summary>
        /// If this member is TRUE, the partition is rewritable. The value of this parameter should be set to TRUE.
        /// </summary>
        public readonly bool RewritePartition;
        /// <summary>
        /// <see cref="PartitionInformationMbr"/> or <see cref="PartitionInformationGpt"/>
        /// </summary>
        private readonly PartitionInformationUnion Info;
        /// <summary>
        /// A <see cref="PartitionInformationMbr"/> structure that specifies partition information specific to master boot record (MBR) disks. The MBR partition format is the standard AT-style format.
        /// </summary>
        public PartitionInformationMbr Mbr => Info; 
        /// <summary>
        /// A <see cref="PartitionInformationGpt"/> structure that specifies partition information specific to GUID partition table (GPT) disks. The GPT format corresponds to the EFI partition format.
        /// </summary>
        public PartitionInformationGpt Gpt => Info;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartingOffset"></param>
        /// <param name="PartitionLength"></param>
        /// <param name="PartitionNumber"></param>
        /// <param name="RewritePartition"></param>
        public PartitionInformationEx(long StartingOffset, long PartitionLength, uint PartitionNumber, bool RewritePartition)
            => (PartitionStyle, this.StartingOffset, this.PartitionLength, this.PartitionNumber, this.RewritePartition, Info)
            = (PartitionStyle.Raw, StartingOffset, PartitionLength, PartitionNumber, RewritePartition, default);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartingOffset"></param>
        /// <param name="PartitionLength"></param>
        /// <param name="PartitionNumber"></param>
        /// <param name="RewritePartition"></param>
        /// <param name="Mbr"></param>
        public PartitionInformationEx(long StartingOffset, long PartitionLength, uint PartitionNumber, bool RewritePartition, PartitionInformationMbr Mbr)
            => (PartitionStyle, this.StartingOffset, this.PartitionLength, this.PartitionNumber, this.RewritePartition, Info)
            = (PartitionStyle.Mbr, StartingOffset, PartitionLength, PartitionNumber, RewritePartition, Mbr);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartingOffset"></param>
        /// <param name="PartitionLength"></param>
        /// <param name="PartitionNumber"></param>
        /// <param name="RewritePartition"></param>
        /// <param name="Gpt"></param>
        public PartitionInformationEx(long StartingOffset, long PartitionLength, uint PartitionNumber, bool RewritePartition, PartitionInformationGpt Gpt)
            => (PartitionStyle, this.StartingOffset, this.PartitionLength, this.PartitionNumber, this.RewritePartition, Info)
            = (PartitionStyle.Mbr, StartingOffset, PartitionLength, PartitionNumber, RewritePartition, Gpt);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PartitionStyle"></param>
        /// <param name="StartingOffset"></param>
        /// <param name="PartitionLength"></param>
        /// <param name="PartitionNumber"></param>
        /// <param name="RewritePartition"></param>
        /// <param name="Mbr"></param>
        /// <param name="Gpt"></param>
        /// <returns></returns>
        public PartitionInformationEx Set(PartitionStyle? PartitionStyle = null, long? StartingOffset = null, long? PartitionLength = null, uint? PartitionNumber = null, bool? RewritePartition = null, PartitionInformationMbr? Mbr = null, PartitionInformationGpt? Gpt = null)
            => PartitionStyle == Disk.PartitionStyle.Raw || (Mbr == null && Gpt == null && this.PartitionStyle == Disk.PartitionStyle.Raw) ? new PartitionInformationEx(StartingOffset ?? this.StartingOffset, PartitionLength ?? this.PartitionLength, PartitionNumber ?? this.PartitionNumber, RewritePartition ?? this.RewritePartition)
            : Mbr != null || PartitionStyle == Disk.PartitionStyle.Mbr || (PartitionStyle == null && this.PartitionStyle == Disk.PartitionStyle.Mbr) ? new PartitionInformationEx(StartingOffset ?? this.StartingOffset, PartitionLength ?? this.PartitionLength, PartitionNumber ?? this.PartitionNumber, RewritePartition ?? this.RewritePartition, Mbr ?? this.Mbr)
            : Gpt != null || PartitionStyle == Disk.PartitionStyle.Gpt || (PartitionStyle == null && this.PartitionStyle == Disk.PartitionStyle.Gpt) ? new PartitionInformationEx(StartingOffset ?? this.StartingOffset, PartitionLength ?? this.PartitionLength, PartitionNumber ?? this.PartitionNumber, RewritePartition ?? this.RewritePartition, Gpt ?? this.Gpt)
            : this;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{nameof(PartitionInformationEx)}{{" +
                $" {nameof(PartitionStyle)}:{PartitionStyle}" +
                $", {nameof(StartingOffset)}:{StartingOffset}" +
                $", {nameof(PartitionLength)}:{PartitionLength}" +
                $", {nameof(PartitionNumber)}:{PartitionNumber}" +
                $", {nameof(RewritePartition)}:{RewritePartition}" +
                $", " + (
                    PartitionStyle == PartitionStyle.Gpt ? $"{nameof(Gpt)}:{Gpt}" :
                    PartitionStyle == PartitionStyle.Mbr ? $"{nameof(Mbr)}:{Mbr}" : "null") +
                $"}}";
        }
        /// <summary>
        /// inner structure <see cref="PartitionInformationMbr"/> or <see cref="PartitionInformationGpt"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private readonly struct PartitionInformationUnion
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 112)]
            private readonly byte[] Info;
            private PartitionInformationUnion(PartitionInformationMbr Mbr) => Info = StructureToBytes(Mbr, Bytes: new byte[112]);
            private PartitionInformationUnion(PartitionInformationGpt Gpt) => Info = StructureToBytes(Gpt, Bytes: new byte[112]);
            public static implicit operator PartitionInformationUnion(in PartitionInformationMbr Mbr) => new PartitionInformationUnion(Mbr);
            public static implicit operator PartitionInformationMbr(in PartitionInformationUnion Union) => Union.Info?.ToStructure<PartitionInformationMbr>() ?? default;
            public static implicit operator PartitionInformationUnion(in PartitionInformationGpt Gpt) => new PartitionInformationUnion(Gpt);
            public static implicit operator PartitionInformationGpt(in PartitionInformationUnion Union) => Union.Info?.ToStructure<PartitionInformationGpt>() ?? default;

        }
    }
}
