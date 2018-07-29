using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential, Size = 8, Pack = 1)]
    public readonly struct PartitionInformationMbr
    {
        /// <summary>
        /// The type of partition. For a list of values, see Disk Partition Types.
        /// </summary>
        public readonly PartitionType PartitionType;

        /// <summary>
        /// If this member is TRUE, the partition is bootable.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool BootIndicator;

        /// <summary>
        /// If this member is TRUE, the partition is of a recognized type.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public readonly bool RecognizedPartition;

        /// <summary>
        /// The number of hidden sectors in the partition.
        /// </summary>
        public readonly uint HiddenSectors;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PartitionType"></param>
        /// <param name="BootIndicator"></param>
        /// <param name="RecognizedPartition"></param>
        /// <param name="HiddenSectors"></param>
        public PartitionInformationMbr(PartitionType PartitionType, bool BootIndicator, bool RecognizedPartition, uint HiddenSectors)
            => (this.PartitionType, this.BootIndicator, this.RecognizedPartition, this.HiddenSectors)
            = (PartitionType, BootIndicator, RecognizedPartition, HiddenSectors);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PartitionType"></param>
        /// <param name="BootIndicator"></param>
        /// <param name="RecognizedPartition"></param>
        /// <param name="HiddenSectors"></param>
        /// <returns></returns>
        public PartitionInformationMbr Set(PartitionType? PartitionType, bool? BootIndicator, bool? RecognizedPartition, uint? HiddenSectors)
            => new PartitionInformationMbr(PartitionType ?? this.PartitionType, BootIndicator ?? this.BootIndicator, RecognizedPartition ?? this.RecognizedPartition, HiddenSectors ?? this.HiddenSectors);
        public override string ToString() => $"{nameof(PartitionInformationMbr)}{{" +
            $" {nameof(PartitionType)}:{PartitionType}" +
            $", {nameof(BootIndicator)}:{BootIndicator}" +
            $", {nameof(RecognizedPartition)}:{RecognizedPartition}" +
            $", {nameof(HiddenSectors)}:{HiddenSectors}" +
            $"}}";
    }
}
