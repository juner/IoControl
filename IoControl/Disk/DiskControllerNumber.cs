using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DiskControllerNumber
    {
        public uint ControllerNumber;
        public uint DiskNumber;
        public override string ToString()
            => $"{nameof(DiskControllerNumber)}{{{nameof(ControllerNumber)}:{ControllerNumber},{nameof(DiskNumber)}:{DiskNumber}}}";
    }
}
