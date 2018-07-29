using System.Runtime.InteropServices;
using static IoControl.Utils.ByteAndStructure;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PartitionInformationEx
    {
        [MarshalAs(UnmanagedType.U4)]
        public PartitionStyle PartitionStyle;
        public long StartingOffset;
        public long PartitionLength;
        public uint PartitionNumber;
        public bool RewritePartition;
        private PartitionInformationUnion Info;
        public PartitionInformationMbr Mbr { get => Info; set => Info = value; }
        public PartitionInformationGpt Gpt { get => Info; set => Info = value; }

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
