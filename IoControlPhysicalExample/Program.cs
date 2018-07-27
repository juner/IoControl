using IoControl;
using IoControl.Disk;
using IoControl.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

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
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.AtaPassThrough));
                    IoControl.AtaPassThroughSmartAttributes(out var Header, out var Attributes);
                    Trace.WriteLine(Header);
                    foreach (var Attribute in Attributes)
                        Trace.WriteLine(Attribute);
                    {
                        var buffer = new byte[512];
                        var Handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                        var DataBuffer = Handle.AddrOfPinnedObject();
                        using (Disposable.Create(Handle.Free))
                        {
                            IoControl.AtaPassThroughDirect(
                                out var Header2,
                                AtaFlags: AtaFlags.DataIn | AtaFlags.NoMultiple,
                                TimeOutValue: 3,
                                DataBuffer: DataBuffer,
                                DataTransferLength: (uint)buffer.Length,
                                Feature: 0xd0,
                                Cylinder: 0xc24f,
                                Command: 0xb0
                            );
                            var Base = IntPtr.Add(DataBuffer, sizeof(ushort));
                            var AttributeSize = Marshal.SizeOf(typeof(SmartAttribute));
                            Trace.WriteLine("\r\ndirect");
                            foreach (var attribute in Enumerable.Range(0, 30)
                                .Select(index => (SmartAttribute)Marshal.PtrToStructure(IntPtr.Add(Base,index * AttributeSize), typeof(SmartAttribute)))
                                .TakeWhile(attr => attr.Id > 0))
                                Trace.WriteLine(attribute);

                        }
                        Trace.WriteLine("bytes: ");
                        Trace.WriteLine($"[{string.Join(" ",(buffer.Select(v => $"{v:X2}")))}]");

                    }
                }catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.AtaPassThrough));
                    IoControl.AtaPassThroughIdentifyDevice(out var Header, out var Identify);
                    Trace.WriteLine(Header);
                    Trace.WriteLine(Identify);

                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(IOControlCode.DiskGetLengthInfo));
                    IoControl.DiskGetLengthInfo(out var disksize);
                    Trace.WriteLine(disksize);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
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
