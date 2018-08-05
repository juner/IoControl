using System;
using System.Runtime.InteropServices;

namespace IoControl.Scanner
{
    /// <summary>
    /// SCSISCAN_CMD 
    /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/scsiscan/ns-scsiscan-_scsiscan_cmd
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ScsiscanCmd {
        public uint Reserved1;
        public uint Size;
        public Srb.SrbFlags SrbFlags;
        public byte CdbLength;
        public byte SenseLength;
        public byte Reserved2;
        public byte Reserved3;
        public uint TransferLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst =16)]
        public byte[] Cdb;
        public IntPtr pSrbStatus;
        public IntPtr pSenseBuffer;
    }
}
