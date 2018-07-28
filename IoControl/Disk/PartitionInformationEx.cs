using System.Runtime.InteropServices;

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
        public byte[] Info;
        public PartitionInformationMbr Mbr { get => ToStructure<PartitionInformationMbr>(Info); set => FromStructure(Info, value); }
        public PartitionInformationGpt Gpt { get => ToStructure<PartitionInformationGpt>(Info); set => FromStructure(Info, value); }
        private static T ToStructure<T>(byte[] Bytes, int StartIndex = default){
            var Size = Marshal.SizeOf<T>();
            var Ptr = Marshal.AllocCoTaskMem(Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
            {
                Marshal.Copy(Bytes, StartIndex, Ptr, Size);
                return (T)Marshal.PtrToStructure(Ptr, typeof(T));
            }
        }
        private static void FromStructure<T>(byte[] Bytes, T Structure, int StartIndex = default)
        {
            var Size = Marshal.SizeOf<T>();
            var Ptr = Marshal.AllocCoTaskMem(Size);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(Ptr)))
            {
                Marshal.StructureToPtr(Structure, Ptr, false);
                Marshal.Copy(Ptr, Bytes, StartIndex, Size);
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
