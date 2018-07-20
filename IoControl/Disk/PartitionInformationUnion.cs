using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct PartitionInformationUnion
    {
        [FieldOffset(0)]
        public PartitionInformationGpt Gpt;
        [FieldOffset(0)]
        public PartitionInformationMbr Mbr;
    }
}
