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
            public static extern VolumeSafeHandle FindFirstVolume([Out] char[] lpszVolumeName, uint cchBufferLength);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool FindNextVolume(VolumeSafeHandle hFindVolume, [Out] char[] lpszVolumeName, uint cchBufferLength);
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
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern VolumeMountPointSafeHandle FindFirstVolumeMountPoint(string RootPathName, char[] VolumeMountPoint, uint BufferLength);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool FindNextVolumeMountPoint(VolumeMountPointSafeHandle handle, char[] VolumeMountPoint, uint BufferLength);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool FindVolumeMountPointClose(IntPtr FindVolumeMountPoint);
            [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public extern static bool GetVolumeInformation(
                string rootPathName,
                StringBuilder volumeNameBuffer,
                int volumeNameSize,
                out uint volumeSerialNumber,
                out uint maximumComponentLength,
                out FileSystemFeature fileSystemFlags,
                StringBuilder fileSystemNameBuffer,
                int nFileSystemNameSize);
            [DllImport("Shlwapi.dll", SetLastError = true)]
            public extern static bool PathFileExists(string Path);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeviceName"></param>
        /// <returns></returns>
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
                        return Split(Marshal.PtrToStringAnsi(mem, (int)ReturnSize).ToCharArray(), '\0');
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
        public static IEnumerable<string> FindVolumes() => VolumeSafeHandle.Find();
        /// <summary>
        /// <see cref="FindVolumes"/> のハンドル用
        /// </summary>
        private class VolumeSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private VolumeSafeHandle() : base(true) { }
            public VolumeSafeHandle(IntPtr preexistingHandle, bool ownsHandle)
            : base(ownsHandle) => SetHandle(preexistingHandle);
            protected override bool ReleaseHandle() => NativeMethods.FindVolumeClose(handle);
            /// <summary>
            /// 
            /// </summary>
            /// <param name="Volume"></param>
            /// <returns></returns>
            public static VolumeSafeHandle FindFirst(char[] Volume) => NativeMethods.FindFirstVolume(Volume, (uint)(Volume?.Length ?? 0));
            /// <summary>
            /// 
            /// </summary>
            /// <param name="Volume"></param>
            /// <returns></returns>
            public bool FindNext(char[] Volume) => NativeMethods.FindNextVolume(this, Volume, (uint)(Volume?.Length ?? 0));
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<string> Find() => FindUtil(new char[1024], FindFirst, (handle, buffer) => handle.FindNext(buffer));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RootPathName"></param>
        /// <returns></returns>
        public static IEnumerable<string> FindVolumeMountPoints(string RootPathName) => VolumeMountPointSafeHandle.Find(RootPathName);
        /// <summary>
        /// <see cref="FindVolumeMountPoints"/> のハンドル用
        /// </summary>
        private class VolumeMountPointSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private VolumeMountPointSafeHandle() : base(true) { }
            public VolumeMountPointSafeHandle(IntPtr preexistingHandle, bool ownsHandle)
                : base(ownsHandle) => SetHandle(preexistingHandle);
            protected override bool ReleaseHandle() => NativeMethods.FindVolumeMountPointClose(handle);
            public static VolumeMountPointSafeHandle FindFirst(string RootPathName, char[] VolumeMountPoint) => NativeMethods.FindFirstVolumeMountPoint(RootPathName, VolumeMountPoint, (uint)(VolumeMountPoint?.Length ?? 0));
            public bool FindNext(char[] VolumeMountPoint) => NativeMethods.FindNextVolumeMountPoint(this, VolumeMountPoint, (uint)(VolumeMountPoint?.Length ?? 0));
            public static IEnumerable<string> Find(string RootPathName) => FindUtil(new char[1024], buffer => FindFirst(RootPathName, buffer), (handle, buffer) => handle.FindNext(buffer));

        }
        /// <summary>
        /// Find系のEnumeratingメソッドの簡略用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Buffer"></param>
        /// <param name="FirstMethod"></param>
        /// <param name="NextMethod"></param>
        /// <returns></returns>
        private static IEnumerable<string> FindUtil<T>(char[] Buffer, Func<char[],T> FirstMethod, Func<T,char[],bool> NextMethod)
            where T : SafeHandle
        {
            const int NOT_MORE_ERROR = unchecked((int)0x80070012);
            const int DIRECTORY_NOT_FOUND = unchecked((int)0x80070003);
            var handle = FirstMethod(Buffer);
            IEnumerable<string> Generator()
            {
                if (handle.IsClosed)
                    throw new ObjectDisposedException(nameof(handle));
                using (handle)
                    do
                    {
                        var str = FirstFindSplit(Buffer, '\0');
                        if (!string.IsNullOrWhiteSpace(str))
                            yield return str;
                    } while (NextMethod(handle, Buffer));
            }
            if (handle.IsInvalid)
                using (handle)
                {
                    var errorCode = Marshal.GetHRForLastWin32Error();
                    if (NOT_MORE_ERROR == errorCode || DIRECTORY_NOT_FOUND == errorCode)
                        return Enumerable.Empty<string>();
                    Marshal.ThrowExceptionForHR(errorCode);
                }
            return Generator();
        }
        /// <summary>
        /// IEnumerable 版の Split
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="splitter"></param>
        /// <returns></returns>
        private static IEnumerable<string> Split(char[] chars, char splitter)
        {
            int prevIndex = 0;
            int nextIndex = 0;
            while ((nextIndex = IndexOf(chars, splitter, prevIndex)) > 0)
            {
                var str = new string(chars, prevIndex, nextIndex - prevIndex);
                if (!string.IsNullOrWhiteSpace(str))
                    yield return str;
                prevIndex = nextIndex + 1;
            }
        }
        /// <summary>
        /// 最初の
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="splitter"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private static string FirstFindSplit(char[] chars, char splitter, int startIndex = 0)
        {
            var index = IndexOf(chars, splitter, startIndex);
            if (index >= 0)
                return new string(chars, startIndex, index);
            return new string(chars, startIndex, chars.Length - startIndex);
        }
        /// <summary>
        /// <paramref name="startIndex"/>以降<paramref name="count"/>文字の範囲に存在する<paramref name="splitter"/>までのindexを取得する
        /// </summary>
        /// <param name="chars">文字列</param>
        /// <param name="splitter">調査対象文字</param>
        /// <param name="startIndex">開始index</param>
        /// <param name="count">調査範囲</param>
        /// <returns>発見したindex。見つからなければ-1を返す</returns>
        private static int IndexOf(char[] chars, char splitter, int startIndex = 0, int count = System.Threading.Timeout.Infinite)
        {
            for (int i = startIndex, imax = count > 0 ? count + startIndex : chars.Length ; i < imax; i++)
            {
                if (chars[i] == splitter)
                    return i;
            }
            return System.Threading.Timeout.Infinite;
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
            return Split(Chars, '\0');
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
            return FirstFindSplit(Chars, '\0');

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
            return Split(buffer, '\0');
        }
        /// <summary>
        /// ボリュームの情報を取得する
        /// </summary>
        /// <param name="RootPathName"></param>
        /// <param name="VolumeName"></param>
        /// <param name="VolumeSerialNumber"></param>
        /// <param name="MaximumComponentLength"></param>
        /// <param name="FileSystemFlags"></param>
        /// <param name="FileSystemName"></param>
        /// <returns></returns>
        public static bool GetVolumeInformation(
                string RootPathName,
                out string VolumeName,
                out uint VolumeSerialNumber,
                out uint MaximumComponentLength,
                out FileSystemFeature FileSystemFlags,
                out string FileSystemName)
        {
            var volumeNameBuffer = new StringBuilder(1024, 1024);
            var fileSystemNameBuffer = new StringBuilder(1024, 1024);
            var result = NativeMethods.GetVolumeInformation(RootPathName, volumeNameBuffer, volumeNameBuffer.MaxCapacity, out VolumeSerialNumber, out MaximumComponentLength, out FileSystemFlags, fileSystemNameBuffer, fileSystemNameBuffer.Length);
            VolumeName = result ? volumeNameBuffer.ToString() : string.Empty;
            FileSystemName =  result ? fileSystemNameBuffer.ToString() : string.Empty;
            return result;
        }
        public static (string VolumeName, uint VolumeSerialNumber, uint MaximumComponentLength, FileSystemFeature FileSystemFlags, string FileSystemName) GetVolumeInformation(string RootPath) {
            if(!GetVolumeInformation(RootPath, out var VolumeName, out var VolumeSerialNumber, out var MaximumComponentLength, out var FileSystemFeature, out var FileSystemName))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            return (VolumeName, VolumeSerialNumber, MaximumComponentLength, FileSystemFeature, FileSystemName);
        }
        public static bool PathFileExists(string Path) {
            var result = NativeMethods.PathFileExists(Path);
            var errCode = Marshal.GetHRForLastWin32Error();
            var exception = Marshal.GetExceptionForHR(errCode);
            return result;
        }
    }
}
