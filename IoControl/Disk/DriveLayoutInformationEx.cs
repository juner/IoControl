using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// DRIVE_LAYOUT_INFORMATION_EX structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_drive_layout_information_ex )
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DriveLayoutInformationEx
    {
        /// <summary>
        /// Takes a <see cref="PartitionStyle"/> enumerated value that specifies the type of partition table the disk contains.
        /// </summary>
        public PartitionStyle PartitionStyle;
        /// <summary>
        /// Indicates the number of partitions detected on the disk.
        /// </summary>
        public uint PartitionCount;
        /// <summary>
        /// <see cref="DriveLayoutInformationMbr"/> or <see cref="DriveLayoutInformationGpt"/>
        /// </summary>
        private DriveLayoutInformationUnion Info;
        /// <summary>
        /// Indicates the drive layout information for a disk with a Master Boot Record. This member is valid when <see cref="PartitionStyle"/> is <see cref="PartitionStyle.Mbr"/>. See the definition of <see cref="DriveLayoutInformationMbr"/> for more information.
        /// </summary>
        public DriveLayoutInformationMbr Mbr { get => Info; set => Info = value; }
        /// <summary>
        /// Indicates the drive layout information for a disk with a GUID Partition Table. This member is valid when <see cref="PartitionStyle"/> is <see cref="PartitionStyle.Gpt"/>. See definition of <see cref="DriveLayoutInformationGpt"/> for more information.
        /// </summary>
        public DriveLayoutInformationGpt Gpt { get => Info; set => Info = value; }
        /// <summary>
        /// Contains a variable-length array of <see cref="PartitionInformationEx"/> structures, one for each partition on the drive.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 0x16)]
        public PartitionInformationEx[] PartitionEntry;
        public override string ToString()
            => $"{nameof(DriveLayoutInformationEx)}{{ {nameof(PartitionStyle)}:{PartitionStyle}, {nameof(PartitionCount)}:{PartitionCount}, {(PartitionStyle == PartitionStyle.Gpt ? $"{nameof(Gpt)}:{Gpt}" : PartitionStyle == PartitionStyle.Mbr ? $"{nameof(Mbr)}:{Mbr}" : null)}, {nameof(PartitionEntry)}:[{string.Join(", ", (PartitionEntry ?? Enumerable.Empty<PartitionInformationEx>()).Take((int)PartitionCount).Select(v => $"{v}"))}] }}";
        /// <summary>
        /// inner structure <see cref="DriveLayoutInformationMbr"/> or <see cref="DriveLayoutInformationGpt"/>
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        private readonly struct DriveLayoutInformationUnion
        {
            [FieldOffset(0)]
            public readonly DriveLayoutInformationMbr Mbr;
            [FieldOffset(0)]
            public readonly DriveLayoutInformationGpt Gpt;
            public DriveLayoutInformationUnion(in DriveLayoutInformationMbr Mbr) : this() => this.Mbr = Mbr;
            public DriveLayoutInformationUnion(in DriveLayoutInformationGpt Gpt) : this() => this.Gpt = Gpt;
            public static implicit operator DriveLayoutInformationUnion(in DriveLayoutInformationMbr Mbr) => new DriveLayoutInformationUnion(Mbr);
            public static implicit operator DriveLayoutInformationMbr(in DriveLayoutInformationUnion Union) => Union.Mbr;
            public static implicit operator DriveLayoutInformationUnion(in DriveLayoutInformationGpt Gpt) => new DriveLayoutInformationUnion(Gpt);
            public static implicit operator DriveLayoutInformationGpt(in DriveLayoutInformationUnion Union) => Union.Gpt;
        }
    }
}
