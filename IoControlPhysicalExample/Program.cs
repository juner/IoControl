using IoControl;
using IoControl.Disk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace IoControlPhysicalExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            foreach (var IoControl in GetPhysicalDrives(
                FileAccess: FileAccess.ReadWrite,
                    FileShare: FileShare.ReadWrite,
                    CreationDisposition: FileMode.Open,
                    FlagAndAttributes: FileAttributes.Normal))
            {

                try
                {
                    Trace.WriteLine(nameof(IOControlCode.DiskGetDriveGeometryEx));
                    IoControl.DiskGetDriveGeometryEx(out var geometry);
                    Trace.WriteLine(geometry);
                }
                catch (Exception e2)
                {
                    Trace.WriteLine(e2);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.DiskGetLengthInfo));
                    IoControl.DiskGetLengthInfo(out var disksize);
                    Trace.WriteLine(disksize);
                }
                catch (Exception e2)
                {
                    Trace.WriteLine(e2);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.DiskGetDriveLayoutEx));
                    IoControl.DiskGetDriveLayoutEx(out var layout);
                    Trace.WriteLine(layout);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
            }
            Console.ReadLine();
        }
        public static IEnumerable<IoControl.IoControl> GetPhysicalDrives(FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagAndAttributes = default)
        {
            bool hasDrive = false;
            foreach (var PhysicalNumber in Enumerable.Range(0, 10))
            {
                var Path = $@"\\.\PhysicalDrive{PhysicalNumber}";
                using (var file = new IoControl.IoControl(Path, FileAccess, FileShare, CreationDisposition, FlagAndAttributes))
                {
                    Trace.WriteLine($"Open {Path} ... {(file.IsInvalid ? "NG" : "OK")}.");
                    if (file.IsInvalid)
                        continue;
                    hasDrive = true;
                    yield return file;
                }
            }
            if (!hasDrive)
                throw new Exception("対象となるドライブがありません。");
        }
    }
}
