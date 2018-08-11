using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DriveLayoutInformationMbr
    {
        public uint Signature;
        public uint CheckSum;
        public override string ToString()
            => $"{nameof(DriveLayoutInformationMbr)}{{{nameof(Signature)}:{Signature}, {nameof(CheckSum)}:{CheckSum}}}";
    }
}
