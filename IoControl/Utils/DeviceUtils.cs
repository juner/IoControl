using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace IoControl.Utils
{
    public static class DeviceUtils
    {
        private static class NativeMethods
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint QueryDosDevice(string DeviceName, IntPtr TargetPath, uint CharMax);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern FindVolumeSafeHandle FindFirstVolume([Out] StringBuilder lpszVolumeName, uint cchBufferLength);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool FindNextVolume(FindVolumeSafeHandle hFindVolume, [Out] StringBuilder lpszVolumeName, uint cchBufferLength);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool FindVolumeClose(IntPtr hFindVolume);
            [DllImport("kernel32.dll", CharSet= CharSet.Unicode, SetLastError = true)]
            public static extern bool GetVolumePathNamesForVolumeNameW(string VolumeName, char[] VolumePathNames, uint BufferLength, out uint ReturnLength);
        }
        public static IEnumerable<string> QueryDocDevice(string DeviceName = default)
        {
            const int ERROR_INSUFFICIENT_BUFFER = unchecked((int)0x8007007A);
            if (string.IsNullOrWhiteSpace(DeviceName))
                DeviceName = null;
            uint ReturnSize = 0;
            uint MaxSize = 100;
            while (ReturnSize == 0)
            {
                IntPtr mem = Marshal.AllocCoTaskMem((int)MaxSize);
                if (mem == IntPtr.Zero)
                    throw new OutOfMemoryException();
                using (Disposable.Create(() => Marshal.FreeCoTaskMem(mem)))
                {
                    ReturnSize = NativeMethods.QueryDosDevice(DeviceName, mem, MaxSize);
                    var ErrorCode = Marshal.GetHRForLastWin32Error();
                    if (ReturnSize != 0)
                        return Split(Marshal.PtrToStringAnsi(mem, (int)ReturnSize),'\0');
                    else if (ErrorCode == ERROR_INSUFFICIENT_BUFFER)
                        MaxSize *= 2;
                    else
                        Marshal.ThrowExceptionForHR(ErrorCode);
                }
            }
            return Enumerable.Empty<string>();
        }
        /// <summary>
        /// ボリューム名を取得する
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetVolumePathNames()
        {
            IEnumerable<string> GetEnumerable(FindVolumeSafeHandle h,StringBuilder v)
            {
                using (h)
                    do
                    {
                        yield return v.ToString();
                    } while (NativeMethods.FindNextVolume(h, v, (uint)v.MaxCapacity));
                yield break;
            }
            const uint bufferLength = 1024;
            var Volume = new StringBuilder((int)bufferLength, (int)bufferLength);
            var handle = NativeMethods.FindFirstVolume(Volume, (uint)Volume.MaxCapacity);
            if (handle.IsInvalid)
                using (handle)
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return GetEnumerable(handle, Volume);
        }
        /// <summary>
        /// FindVolume のハンドル用
        /// </summary>
        private class FindVolumeSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private FindVolumeSafeHandle() : base(true) { }
            public FindVolumeSafeHandle(IntPtr preexistingHandle, bool ownsHandle)
            : base(ownsHandle) => SetHandle(preexistingHandle);
            protected override bool ReleaseHandle() => NativeMethods.FindVolumeClose(handle);
        }
        /// <summary>
        /// IEnumerable 版の Split
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="splitter"></param>
        /// <returns></returns>
        private static IEnumerable<string> Split(string chars, char splitter)
        {
            int prevIndex = 0;
            int nextIndex = 0;
            while ((nextIndex = chars.IndexOf(splitter, prevIndex)) > 0)
            {
                yield return chars.Substring(prevIndex, nextIndex - prevIndex);
                prevIndex = nextIndex + 1;
            }
        }
        /// <summary>
        /// GetVolumePathNamesForVolumeNameW function ( https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getvolumepathnamesforvolumenamew )
        /// Retrieves a list of drive letters and mounted folder paths for the specified volume.
        /// </summary>
        /// <param name="VolumePathName">A volume GUID path for the volume. A volume GUID path is of the form "\?\Volume{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}".</param>
        /// <returns></returns>
        public static IEnumerable<string> GetVolumePathNamesForVolumeName(string VolumePathName)
        {
            NativeMethods.GetVolumePathNamesForVolumeNameW(VolumePathName, null, 0, out var Length);
            var Chars = new char[Length];
            //var Ptr = Marshal.AllocCoTaskMem((int)Length * sizeof(char));
            if (!NativeMethods.GetVolumePathNamesForVolumeNameW(VolumePathName, Chars, Length * sizeof(char), out Length))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return Split(new string(Chars),'\0');
        }
    }
}
