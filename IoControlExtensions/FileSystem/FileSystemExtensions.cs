using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace IoControl.FileSystem
{
    public static class FileSystemExtensions
    {
        /// <summary>
        /// FSCTL_IS_VOLUME_MOUNTED control code ( https://msdn.microsoft.com/en-us/library/windows/desktop/aa364574.aspx )
        /// </summary>
        /// <param name="IoControl"></param>
        /// <returns></returns>
        public static bool FsctlIsVolumeMounted(this IoControl IoControl) => IoControl.DeviceIoControl(IOControlCode.FsctlIsVolumeMounted, out _);
        /// <summary>
        /// FSCTL_DISMOUNT_VOLUME control code ( https://msdn.microsoft.com/en-us/library/windows/desktop/aa364562.aspx )
        /// </summary>
        /// <param name="IoControl"></param>
        public static void FsctlDismountVolume(this IoControl IoControl)
        {
            var result = IoControl.DeviceIoControl(IOControlCode.FsctlDismountVolume, out var _);
            if (!result)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
    }
}
