using IoControl;
using IoControl.Disk;
using IoControl.Controller;
using IoControl.MassStorage;
using IoControl.FileSystem;
using static IoControl.Utils.DeviceUtils;
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
            Trace.WriteLine($"[{string.Join(", ", QueryDocDevice())}]");
            foreach (var IoControl in FindVolumes()
                        .Select(v => v.Replace(@"\\?\", @"\\.\").TrimEnd('\\'))
                    .Concat(
                        QueryDocDevice()
                        .Where(v => v.StartsWith("HarddiskVolume"))
                        .Select(v => $@"\\.\{v}")
                    )
                    .Select(v => new IoControl.IoControl(v, 
                        FileAccess: FileAccess.ReadWrite,
                        FileShare: FileShare.ReadWrite,
                        CreationDisposition: FileMode.Open,
                        FlagsAndAttributes: FileAttributes.Normal))
                    .Using())
            {
                try
                {
                    Trace.WriteLine(IoControl);
                    Trace.WriteLine(nameof(FileSystemExtensions.FsctlIsVolumeMounted));
                    Trace.WriteLine(IoControl.FsctlIsVolumeMounted());

                } catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine($"{nameof(MassStorageExtensions.StorageGetDeviceNumber)}");
                    Trace.WriteLine($"{IoControl.StorageGetDeviceNumber()}");
                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(DiskExtensions.DiskGetPartitionInfoEx));
                    Trace.WriteLine(IoControl.DiskGetPartitionInfoEx());
                }catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
            }
            foreach (var IoControl in GetPhysicalDrives(
                FileAccess: FileAccess.ReadWrite,
                    FileShare: FileShare.ReadWrite,
                    CreationDisposition: FileMode.Open,
                    FlagsAndAttributes: FileAttributes.Normal))
            {
                try
                {
                    Trace.WriteLine(nameof(MassStorageExtensions.StorageGetDeviceNumber));
                    IoControl.StorageGetDeviceNumber(out var number);
                    Trace.WriteLine(number);
                }catch(Exception e)
                {
                    Trace.WriteLine(e);
                }
                try
                {
                    Trace.WriteLine(nameof(DiskExtensions.DiskGetDriveGeometryEx2));
                    IoControl.DiskGetDriveGeometryEx2(out var geometry);
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
        public static IEnumerable<IoControl.IoControl> GetPhysicalDrives(FileAccess FileAccess = default, FileShare FileShare = default, FileMode CreationDisposition = default, FileAttributes FlagsAndAttributes = default)
        {
            bool hasDrive = false;
            foreach (var DeviceName in QueryDocDevice().Where(v => v.IndexOf("PhysicalDrive") == 0))
            {
                var Path = $@"\\.\{DeviceName}";
                using (var file = new IoControl.IoControl(Path, FileAccess, FileShare, CreationDisposition, FlagsAndAttributes))
                {
                    Trace.WriteLine($"Open {file} ... {(file.IsInvalid ? "NG" : "OK")}.");
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
