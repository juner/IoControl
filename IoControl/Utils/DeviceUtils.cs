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
            /// <summary>
            /// アプリケーションで MS-DOS デバイス名に関する情報を取得できるようにします。関数は、特定の MS-DOS デバイス名に対する現在のマッピングを取得します。また、既存の MS-DOS デバイス名のリストをすべて取得することもできます。
            /// MS-DOS デバイス名は、オブジェクトの名前空間にシンボリックリンクとして格納されます。MS-DOS パスを対応するパスに変換するコードでは、これらのシンボリックリンクを使って MS-DOS デバイス名とドライブ名をマップします。<see cref="QueryDosDevice(string, IntPtr, uint)"/> 関数を使うと、Win32 ベースのアプリケーションで MS-DOS デバイスの名前空間を実装するためのシンボリックリンク名と各シンボリックリンクの値を照会することができます。
            /// https://msdn.microsoft.com/ja-jp/library/cc429649.aspx
            /// </summary>
            /// <param name="DeviceName">照会する MS-DOS デバイス名文字列へのポインタを指定します。このパラメータには、NULL を指定することができます。その場合、<see cref="QueryDosDevice(string, IntPtr, uint)"/> 関数は既存の MS-DOS デバイス名をすべて列挙したリストを、<paramref name="TargetPath"/> パラメータが示すバッファに格納します。 </param>
            /// <param name="TargetPath">会結果を受け取るバッファへのポインタを指定します。このバッファには、1 つ以上の NULL で終わる文字列が入ります。最後の NULL で終わる文字列の後ろには、さらに NULL 文字が追加されます。<paramref name="DeviceName"/> パラメータが NULL 以外の場合、lpDeviceName パラメータが示す特定の MS-DOS デバイスに関する情報が返ります。最初にバッファに保存される NULL で終わる文字列は、現在のデバイス名マッピングです。その他の NULL で終わる文字列は、削除されていない以前のデバイス名マッピングです。 <paramref name="DeviceName"/> パラメータが NULL の場合、既存の MS-DOS デバイス名をすべて列挙したリストが返ります。バッファに格納される NULL で終わる各文字列は、既存の MS-DOS デバイス名を表します。 </param>
            /// <param name="CharMax"></param>
            /// <returns></returns>
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint QueryDosDevice(string DeviceName, IntPtr TargetPath, uint CharMax);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern FindVolumeSafeHandle FindFirstVolume([Out] StringBuilder lpszVolumeName, uint cchBufferLength);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool FindNextVolume(FindVolumeSafeHandle hFindVolume, [Out] StringBuilder lpszVolumeName, uint cchBufferLength);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool FindVolumeClose(IntPtr hFindVolume);
            /// <summary>
            /// GetVolumePathNamesForVolumeNameW function
            /// Retrieves a list of drive letters and mounted folder paths for the specified volume.
            /// https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getvolumepathnamesforvolumenamew
            /// </summary>
            /// <param name="VolumeName">A volume GUID path for the volume. A volume GUID path is of the form "\?\Volume{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}".</param>
            /// <param name="VolumePathNames">The length of the lpszVolumePathNames buffer, in TCHARs, including all NULL characters.</param>
            /// <param name="BufferLength">If the call is successful, this parameter is the number of TCHARs copied to the lpszVolumePathNames buffer. Otherwise, this parameter is the size of the buffer required to hold the complete list, in TCHARs.</param>
            /// <param name="ReturnLength"></param>
            /// <returns></returns>
            [DllImport("kernel32.dll", CharSet= CharSet.Unicode, SetLastError = true)]
            public static extern bool GetVolumePathNamesForVolumeNameW(string VolumeName, char[] VolumePathNames, uint BufferLength, out uint ReturnLength);
            /// <summary>
            /// GetVolumeNameForVolumeMountPointW function
            /// Retrieves a volume GUID path for the volume that is associated with the specified volume mount point ( drive letter, volume GUID path, or mounted folder).
            /// https://docs.microsoft.com/ja-jp/windows/desktop/api/fileapi/nf-fileapi-getvolumenameforvolumemountpointw
            /// </summary>
            /// <param name="VolumeMountPoint">A pointer to a string that contains the path of a mounted folder (for example, "Y:\MountX") or a drive letter (for example, "X:"). The string must end with a trailing backslash ('').</param>
            /// <param name="VolumeName">A pointer to a string that receives the volume GUID path. This path is of the form "\?\Volume{GUID}" where GUID is a GUID that identifies the volume. If there is more than one volume GUID path for the volume, only the first one in the mount manager's cache is returned.</param>
            /// <param name="BufferLength">The length of the output buffer, in TCHARs. A reasonable size for the buffer to accommodate the largest possible volume GUID path is 50 characters.</param>
            /// <returns></returns>
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool GetVolumeNameForVolumeMountPointW(string VolumeMountPoint, char[] VolumeName, uint BufferLength);
            /// <summary>
            /// 現在利用可能なディスクドライブをビットマスク形式で取得します。

            /// https://msdn.microsoft.com/ja-jp/library/cc429329.aspx
            /// </summary>
            /// <returns></returns>
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint GetLogicalDrives();
            /// <summary>
            /// GetLogicalDriveStrings function
            /// Fills a buffer with strings that specify valid drives in the system.
            /// https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getlogicaldrivestringsw
            /// </summary>
            /// <param name="nBufferLength">The maximum size of the buffer pointed to by lpBuffer, in TCHARs. This size does not include the terminating null character. If this parameter is zero, lpBuffer is not used.</param>
            /// <param name="lpBuffer">A pointer to a buffer that receives a series of null-terminated strings, one for each valid drive in the system, plus with an additional null character. Each string is a device name.</param>
            /// <returns></returns>
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint GetLogicalDriveStrings(uint nBufferLength, [Out] char[] lpBuffer);
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
                var str = chars.Substring(prevIndex, nextIndex - prevIndex);
                if (!string.IsNullOrWhiteSpace(str))
                    yield return str;
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
            return Split(new string(Chars, 0, (int)Length),'\0');
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VolumeMountPoint"></param>
        /// <returns></returns>
        public static string GetVolumeNameForVolumeMountPoint(string VolumeMountPoint)
        {
            var Length = 100u;
            var Chars = new char[Length];
            if (!NativeMethods.GetVolumeNameForVolumeMountPointW(VolumeMountPoint, Chars, Length))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return Split(new string(Chars), '\0').FirstOrDefault();

        }
        /// <summary>
        /// 現在利用可能なディスクドライブをビットマスク形式で取得します。
        /// https://msdn.microsoft.com/ja-jp/library/cc429329.aspx
        /// </summary>
        /// <returns></returns>
        public static uint GetLogicalDrives() => NativeMethods.GetLogicalDrives();
        /// <summary>
        /// 現在利用可能なディスクドライブを マウントされたボリューム名で取得します。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetLogicalDriveStrings()
        {
            const int size = 512;
            char[] buffer = new char[size];
            uint ReturnSize = NativeMethods.GetLogicalDriveStrings(size, buffer);
            if (ReturnSize == 0)
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return Split(new string(buffer, 0, (int)ReturnSize), '\0');
        }
    }
}
