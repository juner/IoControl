using System;
using System.Runtime.InteropServices;

namespace IoControl.Controller
{
    /// <summary>
    /// SCSI_PASS_THROUGH
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ScsiPassThrough
    {
        public ushort Length;
        public byte ScsiStatus;
        public byte PathId;
        public byte TargetId;
        public byte Lun;
        public byte CdbLength;
        public byte SenseInfoLength;
        public byte DataIn;
        public uint DataTransferLength;
        public uint TimeOutValue;
        public IntPtr DataBufferOffset;
        public uint SenseInfoOffset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =16)]
        public byte[] Cdb;
    }
}
