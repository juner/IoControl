using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using static IoControl.Utils.DeviceUtils;

namespace IoControl
{
    public static class IoControlTestUtils
    {   
        /// <summary>
        /// 物理ドライブのパスを生成する
        /// </summary>
        /// <param name="PhysicalDriveNumber"></param>
        /// <returns></returns>
        public static string GetPhysicalDrivePath(int PhysicalDriveNumber) => $@"\\.\PhysicalDrive{PhysicalDriveNumber}";
        /// <summary>
        /// 物理ドライブのパスを生成する
        /// </summary>
        /// <param name="start">開始index</param>
        /// <param name="count">カウント</param>
        /// <returns></returns>
        public static IEnumerable<string> PhysicalDrivePathGenerator(int Start, int Count) => Enumerable.Range(Start, Count).Select(GetPhysicalDrivePath);
        /// <summary>
        /// 物理ドライブのパスを生成する
        /// </summary>
        /// <returns></returns>
        //public static IEnumerable<string> PhysicalDrivePathGenerator() => PhysicalDrivePathGenerator(0, 10);
        public static IEnumerable<string> PhysicalDrivePathGenerator() => QueryDocDevice().Where(DeviceName => DeviceName.IndexOf("PhysicalDrive") == 0).Select(DeviceName => $@"\\.\{DeviceName}");
        public static IEnumerable<string> VolumePathGenerator() => GetVolumePathNames().Select(v => v.Replace(@"\\?\", @"\\.\").TrimEnd('\\'));
        /// <summary>
        /// 論理ドライブのパスを生成する
        /// </summary>
        /// <param name="LogicalDrivePath"></param>
        /// <returns></returns>
        private static string GetLogicalDrivePath(string LogicalDrivePath) => $@"\\.\{LogicalDrivePath}".TrimEnd('\\');
        /// <summary>
        /// 論理ドライブのパスを生成する
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> LogicalDrivePathGenerator() => Environment.GetLogicalDrives().Select(GetLogicalDrivePath);
        /// <summary>
        /// 物理ドライブのパスを元に解放が予約済の<see cref="IoControl"/>を生成する
        /// </summary>
        /// <param name="FileAccess"></param>
        /// <param name="FileShare"></param>
        /// <param name="CreationDisposition"></param>
        /// <param name="FlagAndAttributes"></param>
        /// <returns></returns>
        public static IEnumerable<IoControl> GetPhysicalDrives(FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagAndAttributes = default)
            => Using(GetIoControls(PhysicalDrivePathGenerator(), FileAccess, FileShare, CreationDisposition, FlagAndAttributes));
        /// <summary>
        /// 論理ドライブのパスを元に解放が予約済の<see cref="IoControl"/>を生成する
        /// </summary>
        /// <param name="FileAccess"></param>
        /// <param name="FileShare"></param>
        /// <param name="CreationDisposition"></param>
        /// <param name="FlagAndAttributes"></param>
        /// <returns></returns>
        public static IEnumerable<IoControl> GetLogicalDrives(FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagAndAttributes = default)
            => Using(GetIoControls(LogicalDrivePathGenerator(), FileAccess, FileShare, CreationDisposition, FlagAndAttributes));
        /// <summary>
        /// パスのジェネレータを元に解放が予約済の<see cref="IoControl"/>を生成する。
        /// </summary>
        /// <param name="PathGenerator"></param>
        /// <param name="FileAccess"></param>
        /// <param name="FileShare"></param>
        /// <param name="CreationDisposition"></param>
        /// <param name="FlagAndAttributes"></param>
        /// <returns></returns>
        /// <exception cref="AssertInconclusiveException"><paramref name="PathGenerator"/>により生成されたパスが一つも開けなかった場合</exception>
        public static IEnumerable<IoControl> GetIoControls(this IEnumerable<string> PathGenerator, FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagAndAttributes = default)
        {
            IEnumerable<IoControl> Genarator() {
                foreach (var path in PathGenerator)
                {
                    var file = GetIoControl(path, FileAccess, FileShare, CreationDisposition, FlagAndAttributes);
                    if (file.IsInvalid) {
                        file.Dispose();
                        continue;
                    }
                    yield return file;
                }
            }
            if (PathGenerator == null)
                throw new ArgumentNullException(nameof(PathGenerator));
            return Genarator();
        }
        public static IoControl GetIoControl(string path, FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagAndAttributes = default) => new IoControl(path, FileAccess, FileShare, CreationDisposition, FlagAndAttributes);
        /// <summary>
        /// 解放予約を行う
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Disposables"></param>
        /// <returns></returns>
        public static IEnumerable<T> Using<T>(this IEnumerable<T> Disposables)
            where T: IDisposable
        {
            IEnumerable<T> Generator() {
                foreach (var Disposable in Disposables)
                    using (Disposable)
                        yield return Disposable;
            }
            if (Disposables == null)
                throw new ArgumentNullException(nameof(Disposables));
            return Generator();
        }
    }
}
