using System.Linq;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DriveLayoutInformationEx
    {
        public PartitionStyle PartitionStyle;

        public uint PartitionCount;

        public DriveLayoutInformationUnion DriveLayoutInformaition;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 0x16)]
        public PartitionInformationEx[] PartitionEntry;
        public override string ToString()
            => $"{nameof(DriveLayoutInformationEx)}{{ {nameof(PartitionStyle)}:{PartitionStyle}, {nameof(PartitionCount)}:{PartitionCount}, {nameof(DriveLayoutInformaition)}:{(PartitionStyle == PartitionStyle.Gpt ? DriveLayoutInformaition.Gpt.ToString() : PartitionStyle == PartitionStyle.Mbr ? DriveLayoutInformaition.Mbr.ToString() : null)}, {nameof(PartitionEntry)}:[{string.Join(", ", (PartitionEntry ?? Enumerable.Empty<PartitionInformationEx>()).Take((int)PartitionCount).Select(v => $"{v}"))}] }}";
    }
#endregion
}
