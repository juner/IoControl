using System;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DriveLayoutInformationGpt
    {
        public Guid DiskId;
        public long StartingUsableOffset;
        public long UsableLength;
        public uint MaxPartitionCount;
        public override string ToString()
            => $"{nameof(DriveLayoutInformationGpt)}:{{{nameof(DiskId)}:{DiskId}, {nameof(StartingUsableOffset)}:{StartingUsableOffset}, {nameof(UsableLength)}:{UsableLength}, {nameof(MaxPartitionCount)}:{MaxPartitionCount}}}";
    }
#endregion
}
