using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    /// <summary>
    /// CREATE_DISK structure ( https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntdddisk/ns-ntdddisk-_create_disk )
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CreateDisk{
        [FieldOffset(0)]
        public PartitionStyle PartitionStyle;
        [FieldOffset(4)]
        public CreateDiskMbr Mbr;
        [FieldOffset(4)]
        public CreateDiskGpt Gpt;
        public override string ToString()
            => $"{nameof(CreateDisk)}{{{nameof(PartitionStyle)}:{PartitionStyle}, {(PartitionStyle == PartitionStyle.Mbr ? $"{nameof(Mbr)}:{Mbr}": PartitionStyle == PartitionStyle.Gpt ? $"{nameof(Gpt)}:{Gpt}" : "")}}}";
    }
}
