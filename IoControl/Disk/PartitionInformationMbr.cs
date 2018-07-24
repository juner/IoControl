using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential, Size = 8, Pack = 1)]
    public struct PartitionInformationMbr
    {
        /// <summary>
        /// The type of partition. For a list of values, see Disk Partition Types.
        /// </summary>
        public PartitionType PartitionType;

        /// <summary>
        /// If this member is TRUE, the partition is bootable.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool BootIndicator;

        /// <summary>
        /// If this member is TRUE, the partition is of a recognized type.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool RecognizedPartition;

        /// <summary>
        /// The number of hidden sectors in the partition.
        /// </summary>
        public uint HiddenSectors;
        public override string ToString() => $"{nameof(PartitionInformationMbr)}{{" +
            $" {nameof(PartitionType)}:{PartitionType}" +
            $", {nameof(BootIndicator)}:{BootIndicator}" +
            $", {nameof(RecognizedPartition)}:{RecognizedPartition}" +
            $", {nameof(HiddenSectors)}:{HiddenSectors}" +
            $"}}";
    }
}
