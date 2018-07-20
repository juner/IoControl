using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskPerformance
    {
        public long BytesRead;
        public long BytesWritten;
        public long ReadTime;
        public long WriteTime;
        public long IdleTime;
        public uint ReadCount;
        public uint WriteCount;
        public uint QueueDepth;
        public uint SplitCount;
        public long QueryTime;
        public uint StorageDeviceNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public ushort[] StorageManagerName;
        public override string ToString()
            => $"{nameof(DiskPerformance)}{{{nameof(BytesRead)}:{BytesRead}, {nameof(BytesWritten)}:{BytesWritten}, {nameof(ReadTime)}:{ReadTime}, {nameof(WriteTime)}:{WriteTime}, {nameof(IdleTime)}:{IdleTime}, {nameof(ReadCount)}:{ReadCount}, {nameof(WriteCount)}:{WriteCount}, {nameof(QueueDepth)}:{QueueDepth}, {nameof(SplitCount)}:{SplitCount}, {nameof(QueryTime)}:{QueryTime}, {nameof(StorageDeviceNumber)}:{StorageDeviceNumber}, {nameof(StorageManagerName)}:[{string.Join(" ",(StorageManagerName ?? Enumerable.Empty<ushort>()).Select(v => $"{v:X4}"))}]}}";
    }
}
