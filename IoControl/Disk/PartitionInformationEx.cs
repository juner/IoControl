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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 112)]
        private byte[] Info;
        public PartitionInformationMbr Mbr {
            get => Info?.ToStructure<PartitionInformationMbr>() ?? default;
            set {
                if (Info == null)
                    Info = new byte[112];
                Info.FromStructure(value);
            }
        }
        public PartitionInformationGpt Gpt {
            get => Info?.ToStructure<PartitionInformationGpt>() ?? default;
            set {
                if (Info == null)
                    Info = new byte[112];
                Info.FromStructure(value);
            }
        }
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
    }
}
