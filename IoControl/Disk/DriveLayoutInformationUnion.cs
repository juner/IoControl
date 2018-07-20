using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Explicit)]
    public struct DriveLayoutInformationUnion
    {
        [FieldOffset(0)]
        public DriveLayoutInformationMbr Mbr;

        [FieldOffset(0)]
        public DriveLayoutInformationGpt Gpt;
    }
}
