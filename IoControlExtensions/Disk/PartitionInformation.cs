using System;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// PARTITION_INFORMATION structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_partition_information )
    /// The <see cref="PartitionInformation"/> structure contains partition information for a partition with a traditional AT-style Master Boot Record (MBR).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct PartitionInformation : IEquatable<PartitionInformation>
    {
        /// <summary>
        /// Specifies the offset in bytes on drive where the partition begins.
        /// </summary>
        public readonly long StartingOffset;
        /// <summary>
        /// Specifies the length in bytes of the partition.
        /// </summary>
        public readonly long PartitionLength;
        /// <summary>
        /// Specifies the number of hidden sectors.
        /// </summary>
        public readonly uint HiddenSectors;
        /// <summary>
        /// Specifies the number of the partition.
        /// </summary>
        public readonly uint PartitionNumber;
        /// <summary>
        /// Possible values are as follows:
        /// ##############
        /// </summary>
        public readonly PartitionType PartitionType;
        /// <summary>
        /// Indicates, when <see cref="true"/>, that this partition is a bootable (active) partition for this device. When <see cref="false"/>, this partition is not bootable. This member is set according to the partition list entry boot indicator returned by IoReadPartitionTable.
        /// </summary>
        public readonly bool BootIndicator;
        /// <summary>
        /// Indicates, when <see cref="true"/>, that the system recognized the type of the partition. When <see cref="false"/>, the system did not recognize the type of the partition.
        /// </summary>
        public readonly bool RecognizedPartition;
        /// <summary>
        /// Indicates, when <see cref="true"/>, that the partition information has changed. When <see cref="false"/>, the partition information has not changed. This member has a value of <see cref="true"/> when the partition has changed as a result of an <see cref="IOControlCode.DiskSetDriveLayout"/> IOCTL. This informs the system that the partition information needs to be rewritten.
        /// </summary>
        public readonly bool RewritePartition;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartingOffset"></param>
        /// <param name="PartitionLength"></param>
        /// <param name="HiddenSectors"></param>
        /// <param name="PartitionNumber"></param>
        /// <param name="PartitionType"></param>
        /// <param name="BootIndicator"></param>
        /// <param name="RecognizedPartition"></param>
        /// <param name="RewritePartition"></param>
        public PartitionInformation(long StartingOffset, long PartitionLength, uint HiddenSectors, uint PartitionNumber, PartitionType PartitionType, bool BootIndicator, bool RecognizedPartition, bool RewritePartition)
            => (this.StartingOffset, this.PartitionLength, this.HiddenSectors, this.PartitionNumber, this.PartitionType, this.BootIndicator, this.RecognizedPartition, this.RewritePartition)
            = (StartingOffset, PartitionLength, HiddenSectors, PartitionNumber, PartitionType, BootIndicator, RecognizedPartition, RewritePartition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartingOffset"></param>
        /// <param name="PartitionLength"></param>
        /// <param name="HiddenSectors"></param>
        /// <param name="PartitionNumber"></param>
        /// <param name="PartitionType"></param>
        /// <param name="BootIndicator"></param>
        /// <param name="RecognizedPartition"></param>
        /// <param name="RewritePartition"></param>
        public void Deconstruct(out long StartingOffset, out long PartitionLength, out uint HiddenSectors, out uint PartitionNumber, out PartitionType PartitionType, out bool BootIndicator, out bool RecognizedPartition, out bool RewritePartition)
            => (StartingOffset, PartitionLength, HiddenSectors, PartitionNumber, PartitionType, BootIndicator, RecognizedPartition, RewritePartition)
            = (this.StartingOffset, this.PartitionLength, this.HiddenSectors, this.PartitionNumber, this.PartitionType, this.BootIndicator, this.RecognizedPartition, this.RewritePartition);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => obj is PartitionInformation && Equals((PartitionInformation)obj);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PartitionInformation other) => StartingOffset == other.StartingOffset &&
                   PartitionLength == other.PartitionLength &&
                   HiddenSectors == other.HiddenSectors &&
                   PartitionNumber == other.PartitionNumber &&
                   PartitionType == other.PartitionType &&
                   BootIndicator == other.BootIndicator &&
                   RecognizedPartition == other.RecognizedPartition &&
                   RewritePartition == other.RewritePartition;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = -2113428882;
            hashCode = hashCode * -1521134295 + StartingOffset.GetHashCode();
            hashCode = hashCode * -1521134295 + PartitionLength.GetHashCode();
            hashCode = hashCode * -1521134295 + HiddenSectors.GetHashCode();
            hashCode = hashCode * -1521134295 + PartitionNumber.GetHashCode();
            hashCode = hashCode * -1521134295 + PartitionType.GetHashCode();
            hashCode = hashCode * -1521134295 + BootIndicator.GetHashCode();
            hashCode = hashCode * -1521134295 + RecognizedPartition.GetHashCode();
            hashCode = hashCode * -1521134295 + RewritePartition.GetHashCode();
            return hashCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartingOffset"></param>
        /// <param name="PartitionLength"></param>
        /// <param name="HiddenSectors"></param>
        /// <param name="PartitionNumber"></param>
        /// <param name="PartitionType"></param>
        /// <param name="BootIndicator"></param>
        /// <param name="RecognizedPartition"></param>
        /// <param name="RewritePartition"></param>
        /// <returns></returns>
        public PartitionInformation Set(long? StartingOffset = null, long? PartitionLength = null, uint? HiddenSectors = null, uint? PartitionNumber = null, PartitionType? PartitionType = null, bool? BootIndicator = null, bool? RecognizedPartition = null, bool? RewritePartition = null)
            => new PartitionInformation(StartingOffset ?? this.StartingOffset, PartitionLength ?? this.PartitionLength, HiddenSectors ?? this.HiddenSectors, PartitionNumber ?? this.PartitionNumber, PartitionType ?? this.PartitionType, BootIndicator ?? this.BootIndicator, RecognizedPartition ?? this.RecognizedPartition, RewritePartition ?? this.RewritePartition);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{nameof(PartitionInformation)}{{{nameof(StartingOffset)}:{StartingOffset}, {nameof(PartitionLength)}:{PartitionLength}, {nameof(HiddenSectors)}:{HiddenSectors}, {nameof(PartitionNumber)}:{PartitionNumber}, {nameof(PartitionType)}:{PartitionType}, {nameof(BootIndicator)}:{BootIndicator}, {nameof(RecognizedPartition)}:{RecognizedPartition}, {nameof(RewritePartition)}:{RewritePartition}}}";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="information1"></param>
        /// <param name="information2"></param>
        /// <returns></returns>
        public static bool operator ==(PartitionInformation information1, PartitionInformation information2) => information1.Equals(information2);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="information1"></param>
        /// <param name="information2"></param>
        /// <returns></returns>
        public static bool operator !=(PartitionInformation information1, PartitionInformation information2) => !(information1 == information2);
    }
}
