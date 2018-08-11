using System;
using System.Runtime.InteropServices;

namespace IoControl.Scanner
{
    public static class ScannerExtensions
    {
        /// <summary>
        /// IOCTL_SCSISCAN_CMD IOCTL
        /// https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/scsiscan/ni-scsiscan-ioctl_scsiscan_cmd
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool ScsScanCmd(this IoControl IoControl, in ScsiscanCmd cmd, IntPtr Buffer, uint BufferSize)
        {
            var InSize = (uint)Marshal.SizeOf<ScsiscanCmd>();
            IntPtr InPtr = Marshal.AllocCoTaskMem((int)InSize);
            using (Disposable.Create(() => Marshal.FreeCoTaskMem(InPtr)))
            {
                Marshal.StructureToPtr(cmd, InPtr, false);
                return IoControl.DeviceIoControl(IOControlCode.ScsiscanCmd, InPtr, InSize, Buffer, BufferSize, out _);
            }
                
        }
    }
}
