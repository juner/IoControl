using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
namespace IoControl
{
    public static class IoControlTestUtils
    {
        /// <summary>
        /// 物理ドライブのパスを生成する
        /// </summary>
        /// <param name="start">開始index</param>
        /// <param name="count">カウント</param>
        /// <returns></returns>
        public static IEnumerable<string> PhysicalDrivePathGenerator(int start, int count) => Enumerable.Range(start, count).Select(v => $@"\\.\PhysicalDrive{v}");
        /// <summary>
        /// 物理ドライブのパスを生成する
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> PhysicalDrivePathGenerator() => PhysicalDrivePathGenerator(0, 10);
        /// <summary>
        /// 論理ドライブのパスを生成する
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> LogicalDrivePathGenerator() => Environment.GetLogicalDrives().Select(v => $@"\\.\{v}".TrimEnd('\\'));
        /// <summary>
        /// 物理ドライブのパスを元に解放が予約済の<see cref="IoControl"/>を生成する
        /// </summary>
        /// <param name="FileAccess"></param>
        /// <param name="FileShare"></param>
        /// <param name="CreationDisposition"></param>
        /// <param name="FlagAndAttributes"></param>
        /// <returns></returns>
        public static IEnumerable<IoControl> GetPhysicalDrives(FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagAndAttributes = default)
            => GetIoControls(PhysicalDrivePathGenerator(), FileAccess, FileShare, CreationDisposition, FlagAndAttributes);
        /// <summary>
        /// 論理ドライブのパスを元に解放が予約済の<see cref="IoControl"/>を生成する
        /// </summary>
        /// <param name="FileAccess"></param>
        /// <param name="FileShare"></param>
        /// <param name="CreationDisposition"></param>
        /// <param name="FlagAndAttributes"></param>
        /// <returns></returns>
        public static IEnumerable<IoControl> GetLogicalDrives(FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagAndAttributes = default)
            => GetIoControls(LogicalDrivePathGenerator(), FileAccess, FileShare, CreationDisposition, FlagAndAttributes);
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
                    using (var file = new IoControl(path, FileAccess, FileShare, CreationDisposition, FlagAndAttributes))
                    {
                        if (file.IsInvalid)
                            continue;
                        yield return file;
                    }
                }
            }
            return Genarator();
        }
    }
}
