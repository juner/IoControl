using System;
using System.Runtime.InteropServices;

namespace IoControl.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DiskPartitionInfoGpt
    {
        public readonly Guid DiskId;
    }
}
